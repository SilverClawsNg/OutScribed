using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;

namespace OutScribed.Api.Analytics
{
   
    public static class PostgreSqlColumns
    {
        public static Dictionary<string, ColumnWriterBase> General => new()
        {
            ["id"] = new SinglePropertyColumnWriter("Id", PropertyWriteMethod.Raw, NpgsqlDbType.Uuid),
            ["timestamp"] = new TimestampColumnWriter(NpgsqlDbType.Timestamp),
            ["level"] = new LevelColumnWriter(true, NpgsqlDbType.Varchar),
            ["message"] = new RenderedMessageColumnWriter(NpgsqlDbType.Text),
            ["exception"] = new ExceptionColumnWriter(NpgsqlDbType.Text),
            ["properties"] = new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb),
        };

        public static Dictionary<string, ColumnWriterBase> Analytics => new()
        {
            ["id"] = new SinglePropertyColumnWriter("Id", PropertyWriteMethod.Raw, NpgsqlDbType.Uuid),
            ["timestamp"] = new TimestampColumnWriter(NpgsqlDbType.Timestamp),
            ["user_name"] = new SinglePropertyColumnWriter("Username", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["user_id"] = new SinglePropertyColumnWriter("UserId", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["request_path"] = new SinglePropertyColumnWriter("Path", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["method"] = new SinglePropertyColumnWriter("Method", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["status_code"] = new SinglePropertyColumnWriter("StatusCode", PropertyWriteMethod.Raw, NpgsqlDbType.Integer),
            ["elapsed_ms"] = new SinglePropertyColumnWriter("ElapsedMs", PropertyWriteMethod.Raw, NpgsqlDbType.Integer),
            ["user_agent"] = new SinglePropertyColumnWriter("UserAgent", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["properties"] = new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb),
            ["ip_address"] = new SinglePropertyColumnWriter("IpAddress", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["continent"] = new SinglePropertyColumnWriter("Continent", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["country"] = new SinglePropertyColumnWriter("Country", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["region"] = new SinglePropertyColumnWriter("Region", PropertyWriteMethod.ToString, NpgsqlDbType.Text),
            ["city"] = new SinglePropertyColumnWriter("City", PropertyWriteMethod.ToString, NpgsqlDbType.Text),

        };
    }
}
