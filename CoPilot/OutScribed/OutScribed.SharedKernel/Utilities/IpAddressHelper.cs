using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;

namespace OutScribed.SharedKernel.Utilities
{
    /// <summary>
    /// Provides utility methods for extracting IP addresses from an HttpContext.
    /// </summary>
    public static class IpAddressHelper
    {
        /// <summary>
        /// Retrieves the client's IP address from the HttpContext.
        /// Considers X-Forwarded-For header if present (e.g., when behind a proxy/load balancer).
        /// </summary>
        /// <param name="context">The current HttpContext.</param>
        /// <returns>The client's IP address as a string, or null if not found.</returns>
        public static string? GetClientIpAddress(HttpContext context)
        {
            if (context == null)
            {
                return null;
            }

            // Check for X-Forwarded-For header first, which is common when behind a proxy or load balancer.
            // This header can contain multiple IPs, comma-separated. The client's IP is usually the first one.
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                var ipList = forwardedFor.ToString().Split(',').Select(s => s.Trim()).ToList();
                // Take the first IP in the list, as it's typically the client's original IP
                if (ipList.Any() && IPAddress.TryParse(ipList[0], out _))
                {
                    return ipList[0];
                }
            }

            // If X-Forwarded-For is not present or not valid, fall back to Connection.RemoteIpAddress.
            // This is the IP address of the direct remote connection.
            return context.Connection.RemoteIpAddress?.ToString();
        }

        /// <summary>
        /// A more robust method to get the IP address, handling multiple common proxy headers.
        /// </summary>
        /// <param name="context">The current HttpContext.</param>
        /// <returns>The client's IP address as a string, or null if not found.</returns>
        public static string? GetClientIpAddressRobust(HttpContext context)
        {
            if (context == null)
            {
                return null;
            }

            // List of headers to check in order of preference (most external to most internal)
            string[] potentialIpHeaders = new[]
            {
                "X-Forwarded-For",      // Standard header for identifying the original IP address of a client
                "X-Real-IP",            // Commonly used by Nginx proxy
                "CF-Connecting-IP",     // Cloudflare specific header
                "True-Client-IP"        // Akamai specific header
            };

            foreach (var headerName in potentialIpHeaders)
            {
                if (context.Request.Headers.TryGetValue(headerName, out var headerValue))
                {
                    var ipCandidate = headerValue.ToString().Split(',').Select(s => s.Trim()).FirstOrDefault();
                    if (!string.IsNullOrEmpty(ipCandidate) && IPAddress.TryParse(ipCandidate, out _))
                    {
                        return ipCandidate;
                    }
                }
            }

            // Fallback to the direct connection's remote IP address
            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}