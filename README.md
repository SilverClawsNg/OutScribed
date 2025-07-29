# 📚 OutScribed Backend - Project Documentation

**Last Updated:** July 28, 2025
**Project Lead:** OBI
**Status:** In Development

This document serves as the central repository for the backend architecture, domain models, processes, and Read Model definitions for the OutScribed Publishing Platform.

---

## 1.0 Project Overview

This section provides a high-level summary of the project goals, scope, and key components.

### 1.1 Goals
* Allow users to create new accounts
* Allow users to submit applications and be considered as Writers.
* Allow users with writer roles to write, submit, and publish short stories called Tales.
* Allow users to write and submit articles - to address matters arising from Tales - called Insights.
* Allow users to write and submit short articles - sourced from news stories online and which may form foundations of new tales - called Watchlists.
* Allow users to engage with tales, insights, and watchlists through comments, flags, ratings, shares, and follows.
* Allow users to reply, rate, and flag comments
* Allow tagging of tales, insights, and watchlists to relate similar contents

### 1.2 Key Architectural Decisions
* IDE/Language/Framework: Visual Studio, C#, .NET
* Design Principles: Modular Monolith, Domain Driven Design (DDD), Command Query Responsibility Segregation (CQRS)
* Validation: FluentValidation
* API Framework: FastEndpoints (implements REPR).
* Messaging: RabbitMQ. (MassTransit)
* RabbitMQ: Message broker
* PostGresSQL: Write & Read models
* Logging: Serlog
* Caching: Redis
* Documentation & Versioning: Swagger/OpenApi
* Search: ElasticSearch/OpenSearch
* Containerization: Docker
* Orchestration: Kubernetes
* Background Jobs/Scheduling: Hangfire
* Monitoring & Health: Prometheus & Grafana
* Unit testing: xUnit
* Mocking: Moq
* Intergration test: TestContainer
* Business requirement testing: SpecFlow
* Performance monitoring: OpenTelemetry
* API Gateway: Ocelot or Yarp
* Polly: Http calls and retries
* Outbox pattern
* Database-Level Full Text Search 

### 1.3 Modules
* Onboarding : Concerned with user pre-registration
* Analysis : Concerned with Insights
* Discovery : Concerned with Watchlists
* Identity : Concerned with Account Management
* Publishing : Concerned with Tales
* Tagging : Concerned with tagging
* Jail : Concerned with rate limiting

### 1.4. Project/Folder Architexture

This section provides a summary of the project architecture

- [x] src
	- [x] modules (Folder) <!-- Write models for the modules --!>
		- [x] _ModuleName_ (Folder)
			- [x] OutScribed.Modules._ModuleName_.Application <!-- e.g. OutScribed.Modules.Onboarding.Application --!>
				- [x] Features : (Folder) <!-- Commands --!>
	 				- [ ] _FeatureName_  (Folder) <!-- e.g. SendToken --!>
		 			- [ ] _FeatureNameEndpoint_ (Class) <!-- Uses FastEndpoint e.g. SendTokenEndpoint --!>
	 				- [ ] _FeatureNameRequest_ (Class) <!-- Incoming request e.g. SendTokenRequest --!>
	 				- [ ] _FeatureNameResponse_ (Class) <!-- Outgoing response e.g. SendTokenResponse --!>
	 				- [ ] _FeatureNameValidator_ (Class) <!-- Uses FluentValidation e.g. SendTokenValidator --!>
	 				- [ ] _FeatureNameService_ (Class) <!-- Work flow/ Execution e.g. SendTokenService --!>
      				- [x] Repository
          				- [ ] _Module Name_Repository <!-- e.g. DiscoveryRepository --!> 
	 		- [x] OutScribed.Modules._ModuleName_.Domain  <!-- e.g. OutScribed.Modules.Onboarding.Domain --!>
	 			- [ ] Models (folder) <!-- AggregateRoot, entities, value objects --!>
     				- [ ] Enums (folder)
         			- [ ] Exceptions
            			- [ ] Specifications  
	- [x] OutScribed.API (Project) <!-- Starting point of the application --!>
	 	- [ ] Extentions (Folder) <!-- Service extensions --!>
   		- [ ] Middlewares 
	- [x] OutScribed.Application.Queries (Project) <!-- Read models --!>
	 	- [x] DTOs (Folder) 
	 		- [x] _ModuleName_ (Folder) <!-- e.g. Publishing --!>
				- [ ] _DtoName_ (Class) <!-- LoadInsightDetail --!>
	 	- [x] Features (Folder) <!-- Queries --!>
	 		- [x] _ModuleName_ (Folder) <!-- e.g. Tales --!>
				- [x] _FeatureName_  (Folder) <!-- e.g. GetComments --!>
					- [ ] _FeatureNameEndpoint_ (Class) <!-- Uses FastEndpoint --!>
					- [ ] _FeatureNameRequest_ (Class) <!-- Incoming request --!>
					- [ ] _FeatureNameResponse_ (Class) <!-- Outgoing response --!>
					- [ ] _FeatureNameValidator_ (Class) <!-- Uses FluentValidation --!>
					- [ ] _FeatureNameService_ (Class) <!-- Execution --!>
	- [x] OutScribed.Infrastructure (Project) <!-- Database, Email services  --!>
 		- [x] EmailServices (folder)
   		- [x] EventPublishers (folder)
	        - [x] EventConsumers (folder)
         	- [x] BackgroundJobs (folder)
          		- [x] ScheduledJobs (folder)
            		- [x] EnqueuedJobs (folder) 
            		- [ ] HangfireDbContext
		- [x] Persistence (Folder) 
		 	- [x] Reads (Folder) 
				 - [x] Configurations (Folder)
				 	- [ ] ReadsDbContext  (Class)
				 	- [x] _ModuleName_ (Folder) <!-- e.g. Identity --!>
				 		- [ ] _DtoNameConfiguration_ (Class) <!-- e.g. AccountDetailConfiguration --!>
		 	- [x] Writes (Folder) 
		 		- [x] _Module Name_  (Folder) <!-- e.g. Analysis --!>
		 			- [ ] _AggregateRootNameConfiguration_ (Class) <!-- e.g. InsightConfiguration --!>
		 			- [ ] _ModuleNameDbContext_ (Class) <!-- AnalysisDbContext --!>
      		- [x] Repositories
	- [x] OutScribed.SharedKernel (Project) <!-- Common interfaces and classes --!>
	 	- [x] Abstract (Folder)
		- [x] Interfaces
	 	- [x] Enums (Folder) 
	   	- [x] Exceptions (Folder)
    		- [x] Interfaces
	   	- [x] Utilities (Folder) 
			 - [ ] IdGenerator (Class) <!-- Generates ID for entities --!>
			 - [ ] HtmlContentProcessor (Class) <!-- Cleans Html content before storage --!>
			 - [ ] TagsSlugHelper (Class) <!-- Generates a slug from a tag name --!>
    	 		 - [ ] IpAddressHelper (class) <!-- Retrieves Ip Address from HttpContext --!>
			 - [ ] UrlSlugHelper (Class) <!-- Generates a custom URL slug by concatenating title, Year, Month, Day, and a unique short token for uniqueness --!>

- [x] tests

### 1.5 Patterns

* This section discusses patterns used in this application

### 1.5.1 Specification Pattern

* The Specification Pattern is generally considered the most aligned DDD approach for encapsulating querying logic that is relevant to the domain.
* A "Specification" is a self-contained query definition.
- [x] Process
	- [ ] Define a generic ISpecification<T> interface.
	- [ ] Define a base specification abstract class.
 	- [ ] Define a specification evaluator which translates your domain-specific specification object into an executable LINQ query that Entity Framework Core (or any other ORM supporting IQueryable) can understand and translate into SQL.
  	- [ ] Define specification for domain queries as needed

### 1.6 Error Handling

* This section discusses error handling in the application
* There are distinct points where errors can occur: The Endpoint e.g. validation errors, Service, Domain, and Persistence.
* Errors that occurs in the endpoint would be handled by FastEndpoint own errorhandler. It translate the error into a ProblemDetails to be passed to the client.
* The domain should not be allowed to throw exception except in cases that can result in unstable state e.g. Validation error
* Otherwise the domain should return enum values called DomainResults in an enums folder in the domain layer.
* The enums value would clearly indicate the cause of failure but also contain a success value if all goes well
* This would allow the service layer to evaluate the type of exception to throw because the exceotion would correlate with the result returned
* For example, for an enum result called TooManyTokenResends there would exists an exception TooManyTokenResendsException which would be thrown
* This arrangement allows the service to save changes to state, publish any necessary event, and perform other relevant duties before throwing the exception
* A general exception handler middleware woudl be defined to catch all exceptions, log them, and translate into a ProblemDetails to be passed to the client

### 1.7 Database

* This section discusses database design strategy
* PostGresSql would be the database choice at the developmental stage of project
* Three dictinct databases would be created:
  1. OutScribedDb - This would hold act as the primary storage for application data. The database would be divided into Schemas. Every module would have a dedicated schema while the Read module would exist in its own module
  2. OutScribedHangfireDb - This would hold Hangfire jobs (scheduled and enqueued)
  3. OutScribedLogDb - This would hold all logs created by Serilog


---

## 2.0 Jail Module

This section provides description of the ipaddress jailing work flows

### 2.1 Description
* Enforces rate limiting
* Temporarily locks an Ip Address suspected to be involved in an abuse/attack
* Determines when an Ip Address should be locked permanently e.g. number of repeated violations
* Provides mechanism for manually unlocking an Ip Address e.g. based on complaint to Customer Services

### 2.2 Models
- [x] IpAddress (AggregateRoot)
	- [ ] Id (Ulid)
 	- [ ] Value (string) <!-- string representing Ip Address --!>
  	- [ ] CurrentJailReleaseTime (DateTime) <!-- Timespan before released from jail. If permanent ban then DateTime.Max --!>
  	- [ ] IsPermanentlyBanned (DateTime)
  	- [x] JailHistory (Entity) <!-- Keeps record of jails for Ip Address. Can be 1 or multiple --!>
	  	- [ ] JailedAt (DateTime) <!-- Timespan at jail time --!>
	  	- [ ] JailReason (enum) <!-- Reason for jail --!>

### 2.3 Business Rules
* Ip Address involved in up to 5 violations within a 24 hours period is permanently jailed

### 2.4 External Commands
* ReleaseIpAddress <!-- Manually releases a jailed IpAddress.  --!>

### 2.5 ReleaseIpAddress
* Creates a new temporary account, generates and returns a verification token
* Also applies to permanently banned Ip Addresses.

#### 2.5.1 Request
* Id (Ulid)

#### 2.5.2 Response
* Ok

#### 2.5.3 Validation
* Id <!-- Not null. --!>

#### 2.5.4 Work flow
* Receives and validates request
* Gets IpAddress by Id, If null, throws an exception.
* Resets CurrentJailReleaseTime to null and IsPermanentlyJailed to false
* Save changes
* Return response

### 2.6 Internal Services
* ProcessViolation <!-- Handles an Ip Address violation --!>
* IsCurrentlyJailed <!-- Checks if an Ip Address is currently jailed --!>

### 2.7 ProcessViolation
* Jails an Ip Address which was reported by other modules for violations through event publishing
* Called by an event consumer whuch subscribes to the event
* A jailed Ip Address jail time is determined by the number of violations encountered in the last 24 hours
* The maximum jail time for an Ip Address is 24 hours except for permanently banned entries with unexpiring jail time
 
#### 2.7.1 Work flow
* Receives an Ip Address (string)
* Checks if the address already exist
	* If false, this is the first violation.
 		* Creates a new entry
   		* Attaches a new history to it 
 	* Else
  		* If permanently banned, it creates and attaches a new history
    		* If this is its threshold number of violations, it is permanently banned 
    		* Else it calculates its jail time, creates and atatches a new history
* If it does not exist, slugify the name and create a new tag
* Returns the Id of the tag

### 2.8 IsCurrentlyJailed
* Checks if an Ip Address is currently in jail
* Called by a middleware which is used in rate limiting to ensure requests from jailed Ip Addresses are truncated

#### 2.7.1 Work flow
* Receives an Ip Address (string)
* Checks if the address already exist
	* If false, it returns false
 	* Else
  		* Checks if permanently banned and returns true
    		* Checks if jail time has expired and returns false else returns true. 

---

## 3. Onboarding Module

This section provides description of the onboarding work flows

### 3.1 Description
* Onboarding handles pre-registration for new users
* It verifies a user through a verification token
* After the pre-registration is completed, the onboarding entries are permanently deleted
* Note: Can/should email address be verified to be valid & active?

### 3.2 Models
- [x] TempUser (AggregateRoot)
	- [ ] Id (Ulid)
	- [ ] EmailAddress (string)
	- [ ] CreatedAt (DateTime)
	- [ ] LastUpdated (DateTime) <!-- Monitors last time a token was resent or token verification attempt was made --!>
	- [ ] VerifiedAttempts (int) <!-- Monitors number of verification attempts --!>
	- [ ] IpAddress (string)
	- [ ] IsVerified (bool) <!-- If true, token is verified --!>
	- [ ] LockUpdates (bool) <!-- If true, no token resends or verifications --!>
	- [ ] LockUntil (DateTime) <!-- Time until lockUpDates is lifted --!>
	- [ ] ResendsCounter (int) <!-- Monitors token resends --!>
	- [ ] One-time-token : ValueObject
		- [ ] Token (int) <!-- Generated --!>
		- [ ] ExpiresAt (DateTime) <!-- Date created + 10 minutes --!>

### 3.3 Business rules
* There must be at least 60 seconds between resends of verification tokens
* There is a limit of 5 verification token resends within 10 minutes span
* There is a limit of 5 verification attempts within 10 minutes span
* Verification tokens cannot be sent to the same IpAddress within a 10 minutes span
* Life span of a verification token is 10 minutes

### 3.4 Commands Summary
* SendToken <!-- Creates account, Sends generated token to user --!>
* ResendToken <!-- Regeneartes a token and sends to user --!>
* VerifyToken <!-- Verifies token on file --!>

### 3.5 SendToken
* Creates a new temporary account, generates and returns a verification token

#### 3.5.1 Request
* EmailAddress (string)
* IpAddress (string) <!-- From HttpRequest --!>

#### 3.5.2 Response
* Id (Ulid) <!-- Id of TempUser --!>

#### 3.5.3 Validation
* EmailAddress <!-- Not null. Not empty. 255. --!>

#### 3.5.4 Work flow
* Receives and validates request
* Checks if at least 3 TempUser has been created from same Ip Address in last 10 minutes. If true, publish an event to jail IpAddress and throw new exception.
* Checks if TempUser by email address already exists
	* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, throw new exception.
	* Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends, save changes and throw new exception.
	* Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5. If true, save changes, and throw new exception.
* Else generates a new verification token and creates a new TempUser
* Schedules an email with verification token
* Save changes
* Returns response

### 3.6 ResendToken
* Generates and returns a verification token

#### 3.6.1 Request
* Id (Ulid) 

#### 3.6.2 Response
* Ok

#### 3.6.3 Validation
* EmailAddress <!-- Not null. Not empty. 255. --!>

#### 3.6.4 Work flow
* Receives and validates request
* Checks if at least 3 TempUser has been created from same Ip Address in last 10 minutes. If true, publish event to jail IpAddress and throw new exception.
* Checks if TempUser exists with Id. If false, throw new exception.
* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, throw new exception.
* Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends, save changes, and throw new exception.
* Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5. If true, lock resends, save changes, and throw new exception.
* Else generates a new verification token and creates a new TempUser
* Schedules an email with verification token
* Save changes
* Returns response

### 3.7 VerifyToken
* Verifies a verification token

#### 3.7.1 Request
* Id (Ulid)
* Token (string)

#### 3.7.2 Response
* Ok

#### 3.7.3 Validation
* Id <!-- Not null. Not empty. --!>

#### 3.7.4 Work flow
* Receives and validates request
* Checks if TempUser exists with Id. If false, throw new exception.
* Checks if TempUser is already verified, throw new exception.
* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, throw new exception.
* Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends, save changes and throw new exception.
* Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5. If true, lock resends, save changes, and throw new exception.
* Compare the verification token, if false, it increments the VerificationAttempts and returns error to frontend.
* Else marks the IsVerified as true.
* Returns request

---

## 4. Identity Module

This section provides description of the identity work flows. 

### 4.1 Description
* Identity handles account creation, password management, user authentication & authorization, role management, profile updates
* It creates a new account after the user has completed the pre-registration an has verified email address
* It lets user updates profile details such as title, bio, photo
* It allows assignment of administrative roles such as superAdmin, contentAdmin, Publisher, etc.
* It allows user to apply for Writer privileges and also allows designated admin PublisherAdmin grant them these privileges
* It allows users to login and logout of account
* It allows users to reset forgotten passwords or update their passwords

### 4.2 Models
- [x] Account  (AggregateRoot)
	- [ ] Id (Ulid)
	- [ ] EmailAddress (string)
	- [ ] RegisteredAt (DateTime) <!-- Timestamp of registration --!>
	- [ ] Username (string) <!-- Unique public identifier --!>
 	- [x] OneTimeToken (ValueObject) <!-- Holds token used in password reset e.g. Token, ExpireAt. --!>
		 - [ ] Token (int) <!-- Generated --!>
		 - [ ] ExpiresAt (DateTime) <!-- Date created + 10 minutes --!>
	- [x] RefreshToken (ValueObject) <!-- Holds token used in JWT authentication e.g. Token, ExpireAt. --!>
		 - [ ] Token (int) <!-- Generated --!>
		 - [ ] ExpiresAt (DateTime) <!-- Date created + 300 minutes --!>
	- [x] Password (ValueObject) <!-- Holds password info e.g. Hash, Salt. --!>
		 - [ ] Hash (string) <!-- Generated from received password --!>
		 - [ ] Salt (string) <!-- Generated --!>
	- [x] Profile (Entity) <!-- Holds profile info e.g. Title, Bio, Photo, IsAnnonymous, etc. --!>
		 - [ ] Title (string) <!-- User's title. No distinction between first, middle, and last name to accomodate businesses, etc. --!>
		 - [ ] AttachedAt (DateTime) <!-- Date profile was first attached --!>
		 - [ ] IsAnnonymous (bool) <!-- If true, user's title, bio, photo, and contact details would not be publicly displayed --!>
		 - [ ] Bio (string) <!-- Html content. Requires cleanup before storage --!>
		 - [ ] Photo (string) <!-- Location of photo on file. Storage is on DigitalOcean Space using AW3 SDK --!>
	- [x] Admin (Entity) <!-- Holds admin e.g. Type, IsActive etc. --!>
		 - [ ] RoleType (enum) <!-- Type of admin e.g. SuperAdmin, ContentModerator, Publisher, etc. --!>
		 - [ ] AssignedAt (DateTime) <!-- Timestamp role was assigned --!>
		 - [ ] IsActive (bool) <!-- If true, role can perform admin activities. Once assigned, admin roles are not removed. ***Is this best policy*** --!>
		 - [ ] LastUpdated (DateTime) <!-- Last admin was deactivated/activated. Defaults to AssignedAt --!>
   		 - [ ] AssignedBy (Ulid) <!-- Id of Admin who assigned role --!>
      	 - [ ] LastUpdatedBy (Ulid) <!-- Id of admin who performed last update. Defaults to AssignedBy --!>
	- [x] LoginHistory (Entity) <!-- Holds info about user's logins e.g. IpAddress, etc. Account can have 0 or multiple LoginHistory --!>
		 - [ ] LoggedAt (DateTime) <!-- Timestamp of login --!>
		 - [ ] IpAddress (string) <!-- IpAddress used in login --!>
	- [x] Contact (Entity) <!-- Holds info about user's contacts e.g. ContactType, etc. Account can have 0 or multiple Contact. --!>
		 - [ ] CreatedAt (DateTime) <!-- Timestamp of first creation --!>
		 - [ ] LastUpdated (DateTime) <!-- Timestamp of last change. Defaults to CreatedAt --!>
		 - [ ] Text (string) <!-- Contact text --!>
		 - [ ] ContactType (enum) <!-- Users can only use pre-defined contact type e.g. Facebook, EmailAddress, etc. --!>
	- [x] Notification (Entity) <!-- Holds info about user's activity logs e.g. NotificationType, etc. Account can have 0 or multiple Notification. --!>
		 - [ ] HappendedAt (DateTime) <!-- Timestamp of activity --!>
   		 - [ ] CausedBy (Ulid) <!-- Id of principal actor --!>
		 - [ ] HasRead (bool) <!-- If true, notification has been read --!>
		 - [ ] Text (string) <!-- Details of notification --!>
		 - [ ] ContactType (enum) <!-- Users can only use pre-defined contact type e.g. Facebook, EmailAddress, etc. --!>
		 - [ ] NotificationType (enum) <!-- Content affected e.g. Account, Tale, etc. --!>
	- [x] Writer (Entity) <!-- Holds writer info e.g. Address, Country, etc. --!>
		 - [ ] Address (string) <!-- Writer's address information --!>
		 - [ ] AppliedAt (DateTime) <!-- Timestamp of application to become writer --!>
		 - [ ] ApprovedAt (DateTime) <!-- Timestamp of approval to become writer --!>
   		 - [ ] LastUpdatedAt (DateTime) <!-- Timestamp of last update --!>
      	 - [ ] ApprovedBy (Ulid) <!-- Id of Admin who approved application --!>
      	 - [ ] LastUpdatedBy (Ulid) <!-- Id of Admin with last update --!>
		 - [ ] IsActive (bool) <!-- If true, has privilege to publish tale --!>
		 - [ ] Country (enum) <!-- Country of origin/location --!>
		 - [ ] Application (string) <!-- Location of writer's application in Adobe PDF on file. Storage is on DigitalOcean Space using AW3 SDK --!>

### 4.3 Business rules
* Duplicate email address and username are not allowed
* There must be at least 60 seconds between resends of verification tokens
* There is a limit of 5 verification token resends within 10 minutes span
* There is a limit of 5 verification attempts within 10 minutes span
* Verification tokens cannot be sent to the same IpAddress within a 10 minutes span
* Life span of a verification token is 10 minutes
* A user can only hold a single role

### 4.4 External Commands
* CreateAccount <!-- Creates new account --!>
* LoginUser <!-- Verifes a user's credentials --!>
* LogoutUser <!-- Resets user's refreshToken, other logout activities. --!>
* UpdateProfile <!-- Updates user's title, bio, etc. --!>
* UpdateContact <!-- Create or update a user's contact --!>
* SendToken <!-- Generates and sends a password reset token --!>
* ResendToken <!-- Generates and resends a password reset token --!>
* ResetPassword <!-- Verifies a password reset token and changes user's password --!>
* ChangePassword <!-- Updates a user's password --!>
* ApplyAsWriter <!-- Attaches a new application to become a writer --!>
* ApproveWriter <!-- Approves a writer application --!>
* UpdateWriter <!-- Updates a writer's privilege --!>
* AssignRole <!-- Assigns a role to a user --!>
* UpdateRole <!-- Updates a user's role e.g. deactivate role or change role --!>
* FollowAccount <!-- Follows an account --!>
* UnfollowAccount <!-- Unfollows an account --!>

### 4.5 CreateAccount
* Creates a new main account. 
* This is second stage of user registration after pre-registration handled by Onboarding module
* This stage assumes the user's email address is verified

#### 4.5.1 Request
* Id (Ulid) <!-- Id of TempUser from onboarding. Attached by frontend --!>
* Username (string)
* Password (string)

#### 4.5.2 Response
* IsSuccessful (bool) <!-- If false, duplicate email address or username was found.

#### 4.5.3 Validation
* Username <!-- Not null. Not empty. 20. --!>
* Password <!-- Not null. Not empty. 8. --!>
* Id <!-- Not null. --!>

#### 4.5.4 Work flow
* Receives and validates request
* Gets TempUser with Id. If no record found, throws an exception.
* If record ws found, checks if account has been verified i.e. IsVerifed == true. If false, throws an exception.
* Checks if duplicate email address or username exists. If record is found, it would be assumed this is a user who have earlier registered. Returns response (IsSuccessful = false)
* Else hash password and generate Salt
* Create a new account
* Create and attach a Notification
* Save changes
* Returns response (IsSuccessFul = true).

### 4.6 LoginAccount
* Logs a user into account 
* After a user is logged in, the title, bio, photo, contacts, etc. is sent back. This would be stored on the storage of user's device for easier retrieval.

#### 4.6.1 Request
* Username (string)
* Password (string)

#### 4.6.2 Response
* Token (string) <!-- Generated and used in JWT authentication --!>
* RefreshToken (string) <!-- Used in JWT authentication --!>
* Title (string)
* Bio (string) 
* Photo (string)
* EmailAddress (string)
* Contacts (List) <!-- List of user's contact i.e. type and text --!>
* RoleType (enum) <!-- 0 if no role --!>
* RegisteredAt (DateTime) <!-- Date of user registration
* ViewsCount (int) <!-- number of proview views --!>
* FollowersCount (int) <!-- Number of followers --!>
* IsAnnonymous (bool)

#### 4.6.3 Validation
* Username <!-- Not null. Not empty. 20. --!>
* Password <!-- Not null. Not empty. 8. --!>

#### 4.6.4 Work flow
* Receives and validates request
* Gets Account by Username. If null, throws an exception.
* Compares Passwords. If false, throws an exception.
* Create and attach a new login history
* Create and attach a new Notification of enum:AccountCreated
* Save changes
* Gets response from repository
* Returns response.

### 4.7 LogoutAccount
* Logs a user out of account
* Resets refreshToken.
  
#### 4.7.1 Request
* Id (Ulid)

#### 4.7.2 Response
* Ok
  
#### 4.7.3 Validation

#### 4.7.4 Work flow
* Receives and validates request
* Gets Account by RefreshToken. If no record found, returns response
* Resets RefreshToken
* Save changes
* Returns response.

### 4.8 UpdateProfile
* Updates profile details such as title, bio, etc.
* Resets refreshToken.

#### 4.8.1 Request
* Id (Ulid)
* Title (string)
* Bio (string) 
* PhotoBase64String (string)
* EmailAddress (string)
* IsAnnonymous (bool)

#### 4.8.2 Response
* Photo (string) <!-- FileName --!>
  
#### 4.8.3 Validation
* Id <!-- Not Null --!>
* Title <!-- Max:128 --!>
* Bio <!-- Max:512 --!>
* EmailAddress (string) <!-- Max:255 --!>

#### 4.8.4 Work flow
* Receives and validates request
* Gets Account by Id. If null, returns response
* If PhotoBase64 is not null, Generate a new Filename, Save file to DigitalOcean Space
* Update Title, Bio, IsAnnonymous, EmailAddress where not null
* Create and attach a new Notification of enum:ProfileUpdated
* Save changes
* Returns response.

### 4.9 UpdateContact
* Updates a contact detail
* Only predefined contact type e.g. Facebook, etc. are allowed.
* If a contact detail with a specific contact type is not already attached, a new one is created

#### 4.9.1 Request
* Id (Ulid)
* ContactType (enum) <!-- e.g. Facebook, Twitter, etc. --!>
* Text (string) 

#### 4.9.2 Response
* Ok

#### 4.9.3 Validation
* Id <!-- Not Null --!>
* Contact Type <!-- Not Null. Must be in enum. --!>
* Text <!-- Not Null. Not Empty. 64 --!>

#### 4.9.4 Work flow
* Receives and validates request
* Gets Account by Id. If null, returns response
* Update contact if already exists else create and attach a new contact
* Create and attach a new Notification of enum:ContactUpdated
* Save Changes
* Returns response.

### 4.10 SendToken
* Generates and returns a verification token
* First step in password reset

#### 4.10.1 Request
* EmailAddress (string)

#### 4.10.2 Response
* Id (Ulid) <!-- Id of newly created TempUser --!>

#### 4.10.3 Validation
* EmailAddress <!-- Not null. Not empty. 255. --!>

#### 4.10.4 Work flow
* Receives and validates request
* Get account bt email address. If it does not exists, throw new exception.
	* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, throw new exception.
	* Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends, save changes and throw new exception.
	* Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5. If true, lock resends, save changes, and throw new exception.
* Else generates a new verification token
* Schedules an email with verification token
* Save changes
* Returns response

### 4.11 ResendToken
* Generates and returns a verification token

#### 4.11.1 Request
* Id (Ulid) 

#### 4.11.2 Response
* Ok

#### 4.11.3 Validation
* EmailAddress <!-- Not null. Not empty. 255.

#### 4.11.4 Work flow
* Receives and validates request
* Get Account by email address. If null, throw an exception.
* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, throw an exception.
* Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends, save changes, and throw an exception.
* Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5. If true, lock resends, save changes, and throw an exception.
* Else generates a new verification token
* Schedules an email with verification token
* Save changes
* Returns response

### 4.12 ResetPassword
* Resets a password
  
#### 4.12.1 Request
* Id (Ulid)
* Password (string)
* Token (string)

#### 4.12.2 Response
* Ok

#### 4.12.3 Validation
* Id <!-- Not null. Not empty. --!>
* Password <!-- Not null. Not empty. Min: 8 --!>
* Token <!-- Not null. Not empty. Size: 6. --!>

#### 4.12.4 Work flow
* Receives and validates request
* Gets Account exists with Id. If null, throw an exception.
* Compares Token. If false, throw an exception.
* Hash password, generate Salt and update.
* Save changes
* Returns request

### 4.13 UpdatePassword
* Changes an existing password
  
#### 4.13.1 Request
* Id (Ulid) <!-- From HttpContext -- !>
* OldPassword (string)
* NewPassword (string)

#### 4.13.2 Response
* Ok

#### 4.13.3 Validation
* Id <!-- Not null. Not empty. --!>
* OldPassword <!-- Not null. Not empty. Min: 8 --!>
* NewPassword <!-- Not null. Not empty. Min: 8 --!>

#### 4.13.4 Work flow
* Receives and validates request
* Gets Account with Id. If null, throw an exception.
* Compares old password. If false, throw an exception.
* Hash new password, generate Salt and update.
* Save changes
* Returns request

### 4.14 ApplyAsWriter
* Accepts an application from a registered user for privileges to create Tales.
  
#### 4.14.1 Request
* Id (Ulid) <!-- From HttpContext -- !>
* Country (enum)
* ApplicationBase64String (string) <!-- Adobe PDF file --!>

#### 4.14.2 Response
* Ok

#### 4.14.3 Validation
* Id <!-- Not null. Not empty. --!>
* Country <!-- Not null. IsEnum. --!>
* ApplicationBase64String <!-- Not null. Not empty. --!>

#### 4.14.4 Work flow
* Receives and validates request
* Gets Account exists with Id. If null, throw an exception.
* Checks if Writer entity is already attached to account. If true, throws an exception.
* Generate a new Filename, Save ApplicationBase64String to DigitalOcean Space
* Creates and attaches a new Writer emtity.
* Save changes
* Returns request

### 4.15 ApproveWriter
* Approves a registered user's application for Writer privileges
* Requires an Admin role of type: SuperAdmin or Publisher to perform this operation
  
#### 4.15.1 Request
* Id (Ulid) <!-- From HttpContext -- !>
* ApplicantId (Ulid)

#### 4.15.2 Response
* Ok

#### 4.15.3 Validation
* Id <!-- Not null. Not empty. --!>
* ApplicantId <!-- Not null. Not empty. --!>

#### 4.15.4 Work flow
* Receives and validates request
* Gets Account with ApplicantId. If null, throw an exception.
* Checks if Writer entity is attached to account. If false, throws an exception.
* Update Writer.
* Save changes
* Returns request

### 4.16 UpdateWriter
* Updates a Writer privilege i.e. toggles IsActive
* Requires an Admin role of type: SuperAdmin or Publisher to perform this operation
  
#### 4.16.1 Request
* Id (Ulid) <!-- From HttpContext -- !>
* ApplicantId (Ulid)
* IsActive (bool)

#### 4.16.2 Response
* Ok

#### 4.16.3 Validation
* Id <!-- Not null. Not empty. --!>
* ApplicantId <!-- Not null. Not empty. --!>

#### 4.16.4 Work flow
* Receives and validates request
* Gets Account with ApplicantId. If null, throw an exception.
* Checks if Writer entity is attached to account. If false, throws an exception.
* Update Writer emtity.
* Save changes
* Returns request

### 4.17 AssignRole
* Assigns an admin role to a registered user
* Requires an Admin role of type: SuperAdmin to perform this operation
  
#### 4.17.1 Request
* Id (Ulid) <!-- From HttpContext -- !>
* AssigneeId (Ulid)
* RoleType (enum)

#### 4.17.2 Response
* Ok

#### 4.17.3 Validation
* Id <!-- Not null. Not empty. --!>
* AssigneeId <!-- Not null. Not empty. --!>
* RoleType <!-- Not null. IsEnum --!>

#### 4.17.4 Work flow
* Receives and validates request
* Gets Account with AssigneeId. If null, throw an exception.
* Checks if Admin entity is already attached. If true, throws an exception.
* Creates an attaches Admin.
* Save changes
* Returns request

### 4.18 UpdateRole
* Updates an admin role type
* Requires an Admin role of type: SuperAdmin to perform this operation
  
#### 4.18.1 Request
* Id (Ulid) <!-- From HttpContext -- !>
* AdminId (Ulid)
* RoleType (enum)

#### 4.18.2 Response
* Ok

#### 4.18.3 Validation
* Id <!-- Not null. Not empty. --!>
* AdimId <!-- Not null. Not empty. --!>
* RoleType <!-- Not null. IsEnum --!>

#### 4.18.4 Work flow
* Receives and validates request
* Gets Account with AdminId. If null, throw an exception.
* Checks if Admin entity is attached. If false, throws an exception.
* Update Admin.
* Save changes
* Returns request

### 4.19 UpdateAdmin
* Updates an admin's privilege i.e. toggles IsActive
* Requires an Admin role of type: SuperAdmin to perform this operation
  
#### 4.19.1 Request
* Id (Ulid) <!-- From HttpContext -- !>
* AdminId (Ulid)
* IsActive (bool)

#### 4.19.2 Response
* Ok

#### 4.19.3 Validation
* Id <!-- Not null. Not empty. --!>
* AssigneeId <!-- Not null. Not empty. --!>
* RoleType <!-- Not null. IsEnum --!>

#### 4.19.4 Work flow
* Receives and validates request
* Gets Account with Id. If null, throw an exception.
* Checks if Admin entity is attached. If false, throws an exception.
* Update Admin.
* Save changes
* Returns request

### 4.20 FollowAccount
* Follows an account for notifications on activities involving the account
* A registered user can only follow an account once i.e. duplicates are not allowed
* This process is reversible

#### 4.20.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 4.20.2 Response
* Ok

#### 4.20.3 Validation
* Id <!-- Not null. --!>

#### 4.20.4 Work flow
* Receives and validates request
* Gets Account by Id. If null, throw exception
* Creates an AccountFollow and attaches to account
* Save changes
* Publish event: AccountFollowed
* Returns response

### 4.21 UnfollowAccount
* Unfollows an account

#### 4.21.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 4.21.2 Response
* Ok

#### 4.21.3 Validation
* Id <!-- Not null. --!>

#### 4.21.4 Work flow
* Receives and validates request
* Gets Account by Id. If null, throw exception
* Gets the attached AccountFollow. If null, throw exception
* Detach the AccountFollow
* Save changes
* Publish event: AccountUnfollowed
* Returns response

---

## 5.0 Tagging Module

This section provides description of the tagging work flows. 

### 5.1 Description
* Tags are used to categorize similar contents such as Tales, Watchlists, and Insights
* Individual contents only holds a logical Id for the tags while tagging holds the only definition

### 5.2 Models
- [x] Tag  (AggregateRoot)
	- [ ] Id (Ulid)
	- [ ] Name (string)
 	- [ ] CreatedAt (DateTime)
  	- [ ] Counter (int) <!-- Keeps tab of number of contents with tag --!> 
  	- [ ] Slug (string) <!-- Generated from the name --!>
  
### 5.3 Business rules
* Duplicate tag names/slugs are not allowed
 
### 5.4 Internal Services
* AddTag <!-- Creates a new tag --!>
* RemoveTag <!-- Removes a tag --!>

### 5.5 AddTag
* Creates a new tag from a name, if it does not exist
* Called by other Modules trying to create a new content tag
 
#### 5.5.1 Work flow
* Receives a name (string)
* Gets Tag with name.
* If it does not exist, slugify the name, and create a new tag
* Else increments the tags counter
* Save changes
* Returns Id of tag

### 5.5 RemoveTag
* Removes an existing tag
* Tags with at least one content attached cannot be removed
 
#### 5.5.1 Work flow
* Receives a name (string)
* Gets Tag with name.
* If it exist and counter == 1 then remove tag
* Save changes
* Else decrements counter

---

## 6.0 Publishing Module

This section provides description of the publishing work flows. 

### 6.1 Description
* Tales are the primary feature of the application
* Tales are paid short stories which follows a unique writing pattern
* Tales can be tagged, commented on, shared, flagged, follwoed, and rated.

### 6.2 Tale Lifecycle
- [ ] Creation <!-- Tales are created by registered users with Writer privilege. At this stage, the tale is only visible to its creator. enum:Created --!>
- [x] Updating <!-- Attributes of tale such as summary, photo, tags, text, etc, are added or updated.  --!>
	- [ ] Post-Creation <!-- Tales can be updated after creation --!>
 	- [ ] Returned by Checker <!-- Tales can be updated if returned by a checker --!>
  	- [ ] Returned by Editor  <!-- Tales can be updated if returned by an editor --!>
  	- [ ] Returned by Publisher  <!-- Tales can be updated if returned by a publisher --!>
- [x] Submission <!-- After updating, a tale is offically submitted --!>
	- [ ] Submitted <!-- Tales can be submitted after creation and updating. enum:Submitted --!>
 	- [ ] Resubmitted to Checker <!-- Tales are resubmitted to checker if it was returned by checker. enum:ResubmittedToChecker --!>
  	- [ ] Resubmitted to Editor <!-- Tales are resubmitted to editor if it was returned by editor. enum:ResubmittedToEditor --!>
  	- [ ] Resubmitted to Publisher <!-- Tales are resubmitted to publisher if it was returned by publisher. enum:ResubmittedToPublisher --!>
- [x] Checking <!-- After submission post-creation or resubmitted to checker, a tale is checked for content legality --!>
	- [ ] Checked <!-- Tales are considered checked if assumed to have passed the minimal level of legal criteria. enum:Checked --!>
 	- [ ] Returned by Checker <!-- Tales can be returned to the creator to review. enum:ReturnedByChecker --!>
  	- [ ] Rejected by Checker <!-- Tales can be rejected by a checker and progress truncated. enum:RejectedByChecker --!>
- [x] Editing <!-- After checked or resubmited to editor, a tale is reviewed for content quality --!>
	- [ ] Edited <!-- Tales are considered edited if they pass the minimal criteria for content quality, enum:Edited --!>
 	- [ ] Returned by Editor <!-- Tales can be returned by an editor to the creator for review. enum:ReturnedByEditor --!>
  	- [ ] Rejected by Editor <!-- Tales can be rejected by an editor and progress truncated. enum:RejectedByEditor --!>
- [x] Publication <!-- After checked or resubmited to editor, a tale is reviewed for marketability --!>
	- [ ] Published <!-- Tales are considered publsihed if they pass the minimal criteria for marketability and can now be available publicly. enum:Published --!>
 	- [ ] Returned by Publisher <!-- Tales can be returned by a publisher to the creator for review. enum:ReturnedByPublisher --!>
  	- [ ] Rejected by Publisher <!-- Tales can be rejected by a publisher and progress truncated. enum:RejectedByPublsiher --!>
     
   
### 6.3 Models
- [x] Tale  (AggregateRoot)
	- [ ] Id (Ulid)
	- [ ] Title (string)
 	- [ ] CreatedAt (DateTime)
  	- [ ] Slug (string) <!-- Genrated from the title --!> 
  	- [ ] Summary (string)
  	- [ ] Text (string)
  	- [ ] Photo (string) <!-- File name of photo on DigitalOcean Space --!>
  	- [ ] CreatorId (Ulid)
  	- [ ] Category (enum)
  	- [ ] Country (enum)
  	- [ ] Status (enum)
  	- [x] Tag (Entity) <!-- Information about attached tags. 0 or multiple --!>
  		- [ ] Id (Ulid) <!-- Id of Tag. --!>
  	 	- [ ] TaleId (Ulid) <!-- Association with Tale --!>
  	 	- [ ] TaggedAt (DateTime) 
  	- [x] Share (Entity) <!-- information about share history of tale. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] SharedAt (DateTime)
	  	- [ ] SharerId (Ulid)
	  	- [ ] ContactType (enum) <!-- Media type e.g. Facebook --!>
	  	- [ ] TaleId (Ulid) <!-- Association with Tale --!>
	  	- [ ] Handle (string) <!-- Contact text e.g. mike@yahoo.com --!>
  	- [x] Flag (Entity) <!-- information about flag history of tale. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] FlaggedAt (DateTime)
	  	- [ ] FlaggerId (Ulid)
	  	- [ ] FlagType (enum) <!-- Flag type e.g. Spam --!>
	  	- [ ] TaleId (Ulid) <!-- Association with Tale --!>
  	- [x] Rating (Entity) <!-- information about ratings for tale. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] RatedAt (DateTime)
	  	- [ ] RaterId (Ulid)
	  	- [ ] RateType (enum) <!-- Rate type e.g. Upvote --!>
	  	- [ ] TaleId (Ulid) <!-- Association with Tale --!>
	- [x] Follow (Entity) <!-- information about followers for tale. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] FollowedAt (DateTime)
	  	- [ ] FollowerId (Ulid)
	  	- [ ] TaleId (Ulid) <!-- Association with Tale --!>
   	- [x] Comment (Entity) <!-- information about comments for tale. 0 or multiple. Recursive. --!>
	  	- [ ] Id (Ulid)
	  	- [ ] CommentedAt (DateTime)
	  	- [ ] CommentatorId (Ulid)
	  	- [ ] ParentId (Ulid) <!-- Parent comment. Null if a root comment. --!>
	  	- [ ] TaleId (Ulid) <!-- Association with Tale --!>

      
### 6.3 Business rules
* Duplicate tag names/slugs are not allowed
* Users are only allowed to rate, flag, follow, or share a tale only once
* Users can comment or share a tale as many times as they wish
* Rating, flagging, sharing, and commenting are irreversible
* A comment can be edited or remove by its creator if it has received no rating
 
### 6.4 External Commands
* CreateTale <!-- Creates a new tale --!>
* UpdateTaleBasic <!-- Updates the basic details of a tale --!>
* UpdateTaleSummary <!-- Updates summary of a tale --!>
* UpdateTaleCountry <!-- Updates country of tale --!>
* AddTaleTag <!-- Adds a new tag to tale --!>
* RemoveTaleTag <!-- Removes a tag from tale --!>
* UpdateTalePhoto <!-- Updates photo of tale --!>
* UpdateTaleText <!-- Updates text of tale --!>
* UpdateTaleStatus <!-- Updates status of tale --!>
* FlagTale <!-- Report a tale for a violation --!>
* ShareTale <!-- Records details of a tale share --!>
* FollowTale <!-- Follows a tale for updates --!>
* UnfollowTale <!-- Unfollows a tale --!>
* RateTale <!-- Rates a tale --!>
* AddTaleComment <!-- Adds a new comment to a tale --!>
* AddTaleReply <!-- Adds a reply to a comment on a tale --!>
* RateTaleComment <!-- Rates a tale comment --!>
* FlagTaleComment <!-- Flags a tale comment --!>
* UpdateTaleComment <!-- Updates a tale comment --!>
* RemoveTaleComment <!-- Removes a tale comment --!>

### 6.5 CreateTale
* Creates a new tale with the basics i.e. a title and category
* Creating tales requires registered users with Writer privilege

#### 6.5.1 Request
* Title (string)
* Category (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.5.2 Response
* Id (Ulid) <!-- Id of newly created tale --!>
* CreatedAt (DateTime)

#### 6.5.3 Validation
* Title <!-- Not null. Not empty. 128. --!>
* Category <!-- Not null. IsInEnum. --!>

#### 6.5.4 Work flow
* Receives and validates request
* Slugifies title and creates a new tale
* Save changes
* Publish event: TaleCreated
* Returns response

### 6.6 UpdateTaleBasics
* Updates the basics of a tale i.e. a title and category
* Basic details of published tales are no longer editable

#### 6.6.1 Request
* Id (Ulid)
* Title (string)
* Category (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.6.2 Response
* Ok

#### 6.6.3 Validation
* Id <!-- Not null. --!>
* Title <!-- Not null. Not empty. 128. --!>
* Category <!-- Not null. IsInEnum. --!>

#### 6.6.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Slugifies title and updates tale
* Save changes
* Publish event: TaleUpdated
* Returns response

### 6.7 UpdateTaleSummary
* Updates the summary of a tale
* Summaries of published tales are no longer editable

#### 6.7.1 Request
* Id (Ulid)
* Summary (string)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.7.2 Response
* Ok

#### 6.7.3 Validation
* Id <!-- Not null. --!>
* Summary <!-- Not null. Not empty. 512. --!>

#### 6.7.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Updates tale
* Save changes
* Publish event: TaleUpdated
* Returns response

### 6.8 UpdateTaleText
* Updates the text of a tale
* Text of published tales are no longer editable

#### 6.8.1 Request
* Id (Ulid)
* Text (string)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.8.2 Response
* Ok

#### 6.8.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 65535. --!>

#### 6.8.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Cleans up Text which contains Html content, and updates tale
* Save changes
* Publish event: TaleUpdated
* Returns response

### 6.9 UpdateTalePhoto
* Updates the photo of a tale
* Photos of published tales are no longer editable

#### 6.9.1 Request
* Id (Ulid)
* PhotoBase64String (string)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.9.2 Response
* Ok

#### 6.9.3 Validation
* Id <!-- Not null. --!>
* PhotoBase64String <!-- Not null. Not empty. --!>

#### 6.9.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Creates a thumbnail and save both files to DigitalOcean Space then update tale
* Save changes
* Publish event: TaleUpdated
* Returns response

### 6.10 UpdateTaleCountry
* Updates the country of a tale
* Country of published tales are no longer editable

#### 6.10.1 Request
* Id (Ulid)
* Country (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.10.2 Response
* Ok

#### 6.10.3 Validation
* Id <!-- Not null. --!>
* Country <!-- Not null. IsInEnum. --!>

#### 6.10.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Updates tale
* Save changes
* Publish event: TaleUpdated
* Returns response

### 6.11 AddTaleTag
* Adds a new tag to a tale

#### 6.11.1 Request
* Id (Ulid)
* Name (string) <!-- Name of tag --!>
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.11.2 Response
* Ok

#### 6.11.3 Validation
* Id <!-- Not null. --!>
* Name <!-- Not null. Not empty. 24. --!>

#### 6.11.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Calls an internal service to register tag and get its Id
* Creates a new TaleTag and attaches to tale
* Updates tale
* Save changes
* Publish event: TaleUpdated, TagAdded
* Returns response

### 6.12 RemoveTaleTag
* Removes existing tag from tale

#### 6.12.1 Request
* Id (Ulid)
* TagId (Ulid) <!-- Id of tag --!>
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.12.2 Response
* Ok

#### 6.12.3 Validation
* Id <!-- Not null. --!>
* TagId <!-- Not null. --!>

#### 6.12.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Gets TaleTag by TagId. If null, throw exception
* Updates tale
* Save changes
* Publish event: TaleUpdated, TagRemoved
* Returns response

### 6.13 UpdateTaleStatus
* Updates the text of a tale
* Handles the stages of a tale's lifecycle from submission to publication
* Different privileges are required for each update of a tale's status
	* Writer - Submitted, ResubmittedToChecker, ResubmittedToEditor, ResubmittedToPublisher
 	* Admin:Checker - Checked, ReturnedByChecker, RejectedByChecker
  	* Admin:Editor - Edited, ReturnedByEditor, RejectedByEditor
  	* Admin:Publsiher - Published, ReturnedByPublisher, RejectedByPublisher
* Events thrown also reflects the status been updated
* If status is Published, tale must already have a title, category, summary, photo, and text

#### 6.13.1 Request
* Id (Ulid)
* TaleStatus (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.13.2 Response
* Ok

#### 6.13.3 Validation
* Id <!-- Not null. --!>
* TaleStatus <!-- Not null. IsInEnum. --!>

#### 6.13.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Updates tale
* Save changes
* Publish event based on status
* Returns response

### 6.14 RateTale
* Rates a tale with a pre-defined type
* Any registered user is allowed to rate a tale
* A registered user can only rate a specific tale once i.e. duplicates are not allowed
* This process is irreversible

#### 6.14.1 Request
* Id (Ulid)
* RateType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.14.2 Response
* Ok

#### 6.14.3 Validation
* Id <!-- Not null. --!>
* RateType <!-- Not null. IsInEnum. --!>

#### 6.14.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Creates a TaleRating and attaches to tale
* Save changes
* Publish event: TaleRated
* Returns response

### 6.15 FlagTale
* Flags a tale with a pre-defined type
* Any registered user is allowed to flag a tale
* A registered user can only flag a specific tale once i.e. duplicates are not allowed
* This process is irreversible

#### 6.15.1 Request
* Id (Ulid)
* FlagType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.15.2 Response
* Ok

#### 6.15.3 Validation
* Id <!-- Not null. --!>
* FlagType <!-- Not null. IsInEnum. --!>

#### 6.15.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Creates a TaleFlag and attaches to tale
* Save changes
* Publish event: TaleFlagged
* Returns response

### 6.16 ShareTale
* Records details of a tale sharing on other social networks
* Any user is allowed to share a tale as many times as possible
* This process is expected to fail gracefully 

#### 6.16.1 Request
* Id (Ulid)
* ContactType (enum)
* Handle (string)
* AccountId (Ulid) <!-- From HttpContext. Not required --!>

#### 6.16.2 Response
* Ok

#### 6.16.3 Validation
* Id <!-- Not null. --!>
* ContactType <!-- Not null. IsInEnum. --!>
* Handle <!-- Not null. Not empty. 255. --!>

#### 6.16.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, return response
* Creates a TaleShare and attaches to tale
* Save changes
* Publish event: TaleShared
* Returns response

### 6.17 FollowTale
* Follows a tale for notifications on activities involving the tale
* A registered user can only follow a tale once i.e. duplicates are not allowed
* This process is reversible

#### 6.17.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 6.17.2 Response
* Ok

#### 6.17.3 Validation
* Id <!-- Not null. --!>

#### 6.17.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception
* Creates a TaleFollow and attaches to tale
* Save changes
* Publish event: TaleFollowed
* Returns response

### 6.17 UnfollowTale
* Unfollows a tale

#### 6.17.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 6.17.2 Response
* Ok

#### 6.17.3 Validation
* Id <!-- Not null. --!>

#### 6.17.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception
* Gets the attached TaleFollow. If null, throw exception
* Detach the TaleFollow
* Save changes
* Publish event: TaleUnfollowed
* Returns response

### 6.18 AddTaleComment
* Adds a comment to a tale
* Registered users can add as many comments as they wish to a tale

#### 6.18.1 Request
* Id (Ulid)
* Text (string)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 6.18.2 Response
* CommentId (Ulid)
* CreatedAt (DateTime)
* Text (string) <!-- After HtmlContent cleanup --!>

#### 6.18.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 6.18.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception
* Cleans text which contains Html content, create a new TaleComment and attaches to tale
* Save changes
* Publish event: TaleCommented
* Returns response

### 6.19 AddTaleReply
* Adds a comment to a tale
* Registered users can add as many comments as they wish to a tale

#### 6.19.1 Request
* Id (Ulid)
* Text (string)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 6.19.2 Response
* CommentId (Ulid)
* CreatedAt (DateTime)
* Text (string) <!-- After HtmlContent cleanup --!>

#### 6.19.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 6.19.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Clean text which contains Html content, creates TaleComment and attach to Comment and tale
* Save changes
* Publish event: TaleCommentReplied
* Returns response

### 6.20 UpdateComment
* Updates text of a comment 
* A comment with any rating can no longer be updated

#### 6.20.1 Request
* Id (Ulid)
* Text (string)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 6.20.2 Response
* Text (string) <!-- After HtmlContent cleanup --!>

#### 6.20.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 6.20.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Clean text which contains html content and updates comment
* Save changes
* Publish event: TaleCommentUpdated
* Returns response

### 6.21 RemoveComment
* Removes an existing comment
* A comment with any rating or replies can no longer be removed except with admin privilege

#### 6.21.1 Request
* Id (Ulid)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 6.21.2 Response
* Ok

#### 6.21.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>

#### 6.21.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Detaches comment
* Save changes
* Publish event: TaleCommentRemoved
* Returns response

### 6.22 RateTaleComment
* Rates a tale comment with a pre-defined type
* Any registered user is allowed to rate a tale comment
* A registered user can only rate a specific tale comment once i.e. duplicates are not allowed
* This process is irreversible

#### 6.22.1 Request
* Id (Ulid)
* CommentId (Ulid)
* RateType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.22.2 Response
* Ok

#### 6.22.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>
* RateType <!-- Not null. IsInEnum. --!>

#### 6.22.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception
* Gets comment by Id. If null, throw exception
* Creates RateTaleComment and attaches to comment
* Save changes
* Publish event: TaleCommentRated
* Returns response

### 6.23 FlagTaleComment
* Flags a tale comment with a pre-defined type
* Any registered user is allowed to flag a tale comment
* A registered user can only flag a specific tale comment once i.e. duplicates are not allowed
* This process is irreversible

#### 6.23.1 Request
* Id (Ulid)
* CommentId (Ulid)
* FlagType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 6.23.2 Response
* Ok

#### 6.23.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>
* FlagType <!-- Not null. IsInEnum. --!>

#### 6.23.4 Work flow
* Receives and validates request
* Gets Tale by Id. If null, throw exception.
* Gets a comment by Id. If null, throw exception
* Creates a TaleCommentFlag and attaches to comment
* Save changes
* Publish event: TaleCommentFlagged
* Returns response

---

## 7.0 Analysis Module

This section provides description of the analysis work flows. 

### 7.1 Description
* Insights are elaborate responses to tales and are used by its creators to raise issues that may have arisen from a tale
* Insights cannot be created independently of a tale
* Any registered user can create an insight i.e. no special privilege is required
* Insights are created as drafts and only privately viewed until their creator decides to take them online
* Once online, an insight can no longer be hidden
* Insights can be rated, flagged, commented on, shared, and tagged
* An insight can be updated as long as it does not have any rating, shares, and comments
 
### 7.2 Models
- [x] Insight  (AggregateRoot)
	- [ ] Id (Ulid)
	- [ ] Title (string)
 	- [ ] TaleId (Ulid) <!-- Association with tale --!> 
 	- [ ] CreatedAt (DateTime)
  	- [ ] Slug (string) <!-- Genrated from the title --!> 
  	- [ ] Summary (string)
  	- [ ] Text (string)
  	- [ ] Photo (string) <!-- File name of photo on DigitalOcean Space --!>
  	- [ ] CreatorId (Ulid)
  	- [ ] Category (enum) <!-- optional --!>
  	- [ ] Country (enum)
  	- [ ] IsOnline (bool) <!-- If true, Insight can be publicly viewed --!>
  	- [x] Tag (Entity) <!-- Information about attached tags. 0 or multiple --!>
  		- [ ] Id (Ulid) <!-- Id of Tag. --!>
  	 	- [ ] InsightId (Ulid) <!-- Association with Insight --!>
  	 	- [ ] TaggedAt (DateTime) 
  	- [x] Share (Entity) <!-- information about share history of insight. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] SharedAt (DateTime)
	  	- [ ] SharerId (Ulid)
	  	- [ ] ContactType (enum) <!-- Media type e.g. Facebook --!>
	  	- [ ] InsightId (Ulid) <!-- Association with Insight --!>
	  	- [ ] Handle (string) <!-- Contact text e.g. mike@yahoo.com --!>
  	- [x] Flag (Entity) <!-- information about flag history of insight. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] FlaggedAt (DateTime)
	  	- [ ] FlaggerId (Ulid)
	  	- [ ] FlagType (enum) <!-- Flag type e.g. Spam --!>
	  	- [ ] InsightId (Ulid) <!-- Association with Insight --!>
  	- [x] Rating (Entity) <!-- information about ratings for insight. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] RatedAt (DateTime)
	  	- [ ] RaterId (Ulid)
	  	- [ ] RateType (enum) <!-- Rate type e.g. Upvote --!>
	  	- [ ] InsightId (Ulid) <!-- Association with Tale --!>
	- [x] Follow (Entity) <!-- information about followers for insight. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] FollowedAt (DateTime)
	  	- [ ] FollowerId (Ulid)
	  	- [ ] InsightId (Ulid) <!-- Association with Insight --!>
   	- [x] Comment (Entity) <!-- information about comments for insight. 0 or multiple. Recursive. --!>
	  	- [ ] Id (Ulid)
	  	- [ ] CommentedAt (DateTime)
	  	- [ ] CommentatorId (Ulid)
	  	- [ ] ParentId (Ulid) <!-- Parent comment. Null if a root comment. --!>
	  	- [ ] InsightId (Ulid) <!-- Association with Insight --!>
 	- [x] Addendum (Entity) <!-- information about addendums for insight. 0 or multiple. --!>
	  	- [ ] Id (Ulid)
	  	- [ ] AddedAt (DateTime)
	  	- [ ] Text (string) 
		- [ ] InsightId (Ulid) <!-- Association with Insight --!>
      
### 7.3 Business rules
* Duplicate tag names/slugs are not allowed
* Users are only allowed to rate, flag, follow, or share an insight only once
* Users can comment or share an insight as many times as they wish
* Rating, flagging, sharing, and commenting are irreversible
* A comment can be edited or remove by its creator if it has received no rating
 
### 7.4 External Commands
* CreateInsight <!-- Creates a new insight --!>
* UpdateInsightBasic <!-- Updates the basic details of an insight --!>
* UpdateInsightSummary <!-- Updates summary of an insight --!>
* UpdateInsightCountry <!-- Updates country of an insight --!>
* AddInsightTag <!-- Adds a new tag to an insight --!>
* RemoveInsightTag <!-- Removes a tag from an insight --!>
* UpdateInsightPhoto <!-- Updates photo of an insight --!>
* UpdateInsightText <!-- Updates text of an insight --!>
* PublishInsight <!-- Publishes an insight --!>
* RemoveInsight <!-- Removes an insight --!>
* FlagInsight <!-- Report an insight for a violation --!>
* ShareInsight <!-- Records details of an insight share --!>
* FollowInsight <!-- Follows an insight for updates --!>
* UnfollowInsight <!-- Unfollows an insight --!>
* RateInsight <!-- Rates an insight --!>
* AddInsightComment <!-- Adds a new comment to an insight --!>
* AddInsightReply <!-- Adds a reply to a comment on an insight --!>
* RateInsightComment <!-- Rates an insight comment --!>
* FlagInsightComment <!-- Flags an insight comment --!>
* UpdateInsightComment <!-- Updates an insight comment --!>
* RemoveInsightComment <!-- Removes an insight comment --!>
* AddInsightAddendum <!-- Adds a new addendum to insight --!>

### 7.5 CreateInsight
* Creates a new insight with the basics i.e. a title and category
* Creating insights requires registered users

#### 7.5.1 Request
* Title (string)
* Category (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.5.2 Response
* Id (Ulid) <!-- Id of newly created tale --!>
* CreatedAt (DateTime)

#### 7.5.3 Validation
* Title <!-- Not null. Not empty. 128. --!>
* Category <!-- Not null. IsInEnum. --!>

#### 7.5.4 Work flow
* Receives and validates request
* Slugifies title and creates a new insight
* Save changes
* Publish event: InsightCreated
* Returns response

### 7.6 UpdateInsightBasics
* Updates the basics of an insight i.e. a title and category
* Published insights with any engagement i.e. rating, addendum, comment can no longer be edited

#### 7.6.1 Request
* Id (Ulid)
* Title (string)
* Category (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.6.2 Response
* Ok

#### 7.6.3 Validation
* Id <!-- Not null. --!>
* Title <!-- Not null. Not empty. 128. --!>
* Category <!-- Not null. IsInEnum. --!>

#### 7.6.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Slugifies title and updates insight
* Save changes
* Publish event: InsightUpdated
* Returns response

### 7.7 UpdateInsightSummary
* Updates the summary of an insight
* Published insights with any engagement i.e. rating, addendum, comment can no longer be edited

#### 7.7.1 Request
* Id (Ulid)
* Summary (string)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.7.2 Response
* Ok

#### 7.7.3 Validation
* Id <!-- Not null. --!>
* Summary <!-- Not null. Not empty. 512. --!>

#### 7.7.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Updates insight
* Save changes
* Publish event: InsightUpdated
* Returns response

### 7.8 UpdateInsightText
* Updates the text of an insight
* Published insights with any engagement i.e. rating, addendum, comment can no longer be edited

#### 7.8.1 Request
* Id (Ulid)
* Text (string)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.8.2 Response
* Ok

#### 7.8.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 65535. --!>

#### 7.8.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Cleans up Text which contains Html content, and updates insight
* Save changes
* Publish event: InsightUpdated
* Returns response

### 7.9 UpdateInsightPhoto
* Updates the photo of an insight
* Published insights with any engagement i.e. rating, addendum, comment can no longer be edited

#### 7.9.1 Request
* Id (Ulid)
* PhotoBase64String (string)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.9.2 Response
* Ok

#### 7.9.3 Validation
* Id <!-- Not null. --!>
* PhotoBase64String <!-- Not null. Not empty. --!>

#### 7.9.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Creates a thumbnail and save both files to DigitalOcean Space then update insight
* Save changes
* Publish event: InsightUpdated
* Returns response

### 7.10 UpdateInsightCountry
* Updates the country of an insight
* Published insights with any engagement i.e. rating, addendum, comment can no longer be edited

#### 7.10.1 Request
* Id (Ulid)
* Country (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.10.2 Response
* Ok

#### 7.10.3 Validation
* Id <!-- Not null. --!>
* Country <!-- Not null. IsInEnum. --!>

#### 7.10.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Updates insight
* Save changes
* Publish event: InsightUpdated
* Returns response

### 7.11 AddInsightTag
* Adds a new tag to an insight

#### 7.11.1 Request
* Id (Ulid)
* Name (string) <!-- Name of tag --!>
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.11.2 Response
* Ok

#### 7.11.3 Validation
* Id <!-- Not null. --!>
* Name <!-- Not null. Not empty. 24. --!>

#### 7.11.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Calls an internal service to register tag and get its Id
* Creates a new InsightTag and attaches to insight
* Updates insight
* Save changes
* Publish event: InsightUpdated, TagAdded
* Returns response

### 7.12 RemoveInsightTag
* Removes existing tag from an insight
* Published insights with any engagement i.e. rating, addendum, comment can no longer be edited

#### 7.12.1 Request
* Id (Ulid)
* TagId (Ulid) <!-- Id of tag --!>
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.12.2 Response
* Ok

#### 7.12.3 Validation
* Id <!-- Not null. --!>
* TagId <!-- Not null. --!>

#### 7.12.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Gets Tag by TagId. If null, throw exception
* Detaches InsightTag
* Save changes
* Publish event: InsightUpdated and TagRemoved
* Returns response

### 7.13 PublishInsight
* Publishes an insight to become publicly available
* This is irreversible

#### 7.13.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.13.2 Response
* Ok

#### 7.13.3 Validation
* Id <!-- Not null. --!>

#### 7.13.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Updates insight
* Save changes
* Publish event: InsightPublished
* Returns response

### 7.14 RemoveInsight
* Removes an insight
* Insights with any engagement i.e. rating, addendum, comment cannot be removed

#### 7.14.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.14.2 Response
* Ok

#### 7.14.3 Validation
* Id <!-- Not null. --!>

#### 7.14.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Removes Insight
* Save changes
* Publish event: InsightRemoved
* Returns response

### 7.15 RateInsight
* Rates an insight with a pre-defined type
* Any registered user is allowed to rate an insight
* A registered user can only rate a specific insight once i.e. duplicates are not allowed
* This process is irreversible

#### 7.15.1 Request
* Id (Ulid)
* RateType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.15.2 Response
* Ok

#### 7.15.3 Validation
* Id <!-- Not null. --!>
* RateType <!-- Not null. IsInEnum. --!>

#### 7.15.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Creates InsightRate and attaches to insight
* Save changes
* Publish event: InsightRated
* Returns response

### 7.16 FlagInsight
* Flags an insight with a pre-defined type
* Any registered user is allowed to flag an insight
* A registered user can only flag a specific insight once i.e. duplicates are not allowed
* This process is irreversible

#### 7.16.1 Request
* Id (Ulid)
* FlagType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.16.2 Response
* Ok

#### 7.16.3 Validation
* Id <!-- Not null. --!>
* FlagType <!-- Not null. IsInEnum. --!>

#### 7.16.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Creates an InsightFlag and attaches to insight
* Save changes
* Publish event: InsightFlagged
* Returns response

### 7.17 ShareInsight
* Records details of an insight sharing on other social networks
* Any user is allowed to share an insight as many times as possible
* This process is expected to fail gracefully 

#### 7.17.1 Request
* Id (Ulid)
* ContactType (enum)
* Handle (string)
* AccountId (Ulid) <!-- From HttpContext. Not required --!>

#### 7.17.2 Response
* Ok

#### 7.17.3 Validation
* Id <!-- Not null. --!>
* ContactType <!-- Not null. IsInEnum. --!>
* Handle <!-- Not null. Not empty. 255. --!>

#### 7.17.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, return response
* Creates an InsightShare and attaches to insight
* Save changes
* Publish event: InsightShared
* Returns response

### 7.18 FollowInsight
* Follows an insight for notifications on activities involving the insight
* A registered user can only follow an insight once i.e. duplicates are not allowed
* This process is reversible

#### 7.18.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 7.18.2 Response
* Ok

#### 7.18.3 Validation
* Id <!-- Not null. --!>

#### 7.18.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Creates an InsightFollow and attaches to insight
* Save changes
* Publish event: InsightFollowed
* Returns response

### 7.18 UnfollowInsight
* Unfollows an insight

#### 7.18.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 7.18.2 Response
* Ok

#### 7.18.3 Validation
* Id <!-- Not null. --!>

#### 7.18.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Gets the InsightFollow attached to insight. If null, throw exception
* Detaches InsightFollow
* Save changes
* Publish event: InsightUnfollowed
* Returns response

### 7.19 AddInsightComment
* Adds a comment to an insight
* Registered users can add as many comments as they wish to an insight

#### 7.19.1 Request
* Id (Ulid)
* Text (string)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 7.19.2 Response
* CommentId (Ulid)
* CreatedAt (DateTime)
* Text (string) <!-- After HtmlContent cleanup --!>

#### 7.19.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 7.19.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Cleans text which contains Html content, create a InsightComment and attaches to insight
* Save changes
* Publish event: InsightCommented
* Returns response

### 7.20 AddInsightReply
* Adds a comment to an insight
* Registered users can add as many comments as they wish to an insight

#### 7.20.1 Request
* Id (Ulid)
* Text (string)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 7.20.2 Response
* CommentId (Ulid)
* CreatedAt (DateTime)
* Text (string) <!-- After HtmlContent cleanup --!>

#### 7.20.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 7.20.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Clean text which contains Html content, creates InsightComment and attaches to comment and insight
* Save changes
* Publish event: InsightCommentReplied
* Returns response

### 7.21 UpdateComment
* Updates text of a comment 
* A comment with any rating can no longer be updated

#### 7.21.1 Request
* Id (Ulid)
* Text (string)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 7.21.2 Response
* Text (string) <!-- After HtmlContent cleanup --!>

#### 7.21.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 7.21.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Clean text which contains html content and updates comment
* Save changes
* Publish event: InsightCommentUpdated
* Returns response

### 7.22 RemoveComment
* Removes an existing comment
* A comment with any rating or replies can no longer be removed except with admin privilege

#### 7.22.1 Request
* Id (Ulid)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 7.22.2 Response
* Ok

#### 7.22.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>

#### 7.22.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Detaches comment
* Save changes
* Publish event: TaleCommentRemoved
* Returns response

### 7.23 RateTaleComment
* Rates an insight comment with a pre-defined type
* Any registered user is allowed to rate an insight comment
* A registered user can only rate a specific insight comment once i.e. duplicates are not allowed
* This process is irreversible

#### 7.23.1 Request
* Id (Ulid)
* CommentId (Ulid)
* RateType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.23.2 Response
* Ok

#### 7.23.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>
* RateType <!-- Not null. IsInEnum. --!>

#### 7.23.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Gets comment by Id. If null, throw exception
* Creates InsightCommentRate and attaches to comment
* Save changes
* Publish event: InsightCommentRated
* Returns response

### 7.24 FlagInsightComment
* Flags an insight comment with a pre-defined type
* Any registered user is allowed to flag an insight comment
* A registered user can only flag a specific insight comment once i.e. duplicates are not allowed
* This process is irreversible

#### 7.24.1 Request
* Id (Ulid)
* CommentId (Ulid)
* FlagType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 7.24.2 Response
* Ok

#### 7.24.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>
* FlagType <!-- Not null. IsInEnum. --!>

#### 7.24.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception.
* Gets a comment by Id. If null, throw exception
* Creates InsightCommentFlag and attaches to comment
* Save changes
* Publish event: InsightCommentFlagged
* Returns response

---

## 8.0 Discovery Module

This section provides description of the discovery work flows. 

### 8.1 Description
* Watchlists are summary of articles from trusted news sources across the internet and highlight stories of interest been monitored by Writers
* Watchlists may form the foundations of future Tales however they can exist independently
* Any registered user can create a watchlist i.e. no special privilege is required
* Watchlists are created publication-ready i.e. once created they become visible online
* Watchlists can be rated, flagged, followed, and tagged
* A watchlist can be updated or removed as long as it does not have any rating, shares, and tales
 
### 8.2 Models
- [x] Watchlist  (AggregateRoot)
	- [ ] Id (Ulid)
	- [ ] Title (string)
 	- [ ] CreatedAt (DateTime)
  	- [ ] Slug (string) <!-- Genrated from the title --!> 
  	- [ ] Summary (string)
  	- [ ] SourceText (string)
  	- [ ] SourceUrl (string)
  	- [ ] CreatorId (Ulid)
  	- [ ] Category (enum) 
  	- [ ] Country (enum) <!-- optional --!>
  	- [x] Tag (Entity) <!-- Information about attached tags. 0 or multiple --!>
  		- [ ] Id (Ulid) <!-- Id of Tag. --!>
  	 	- [ ] WatchlistId (Ulid) <!-- Association with Watchlist --!>
  	 	- [ ] TaggedAt (DateTime)
  	- [x] Flag (Entity) <!-- information about flag history of insight. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] FlaggedAt (DateTime)
	  	- [ ] FlaggerId (Ulid)
	  	- [ ] FlagType (enum) <!-- Flag type e.g. Spam --!>
	  	- [ ] WatchlistId (Ulid) <!-- Association with Watchlist --!>
  	- [x] Rating (Entity) <!-- information about ratings for insight. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] RatedAt (DateTime)
	  	- [ ] RaterId (Ulid)
	  	- [ ] RateType (enum) <!-- Rate type e.g. Upvote --!>
	  	- [ ] WatchlistId (Ulid) <!-- Association with Watchlist --!>
	- [x] Follow (Entity) <!-- information about followers for insight. 0 or multiple --!>
	  	- [ ] Id (Ulid)
	  	- [ ] FollowedAt (DateTime)
	  	- [ ] FollowerId (Ulid)
	  	- [ ] WatchlistId (Ulid) <!-- Association with Watchlist --!>
   	- [x] Comment (Entity) <!-- information about comments for insight. 0 or multiple. Recursive. --!>
	  	- [ ] Id (Ulid)
	  	- [ ] CommentedAt (DateTime)
	  	- [ ] CommentatorId (Ulid)
	  	- [ ] ParentId (Ulid) <!-- Parent comment. Null if a root comment. --!>
	  	- [ ] WatchlistId (Ulid) <!-- Association with Watchlist --!>
 	- [x] LinkedTale (Entity) <!-- information about tales linked to this watchlist. 0 or multiple. --!>
	  	- [ ] Id (Ulid) <!-- TaleId --!>
	  	- [ ] LinkedAt (DateTime)
		- [ ] WatchlistId (Ulid) <!-- Association with Watchlist --!>
      
### 8.3 Business rules
* Duplicate tag names/slugs are not allowed
* Users are only allowed to rate, flag, or follow a watchlist only once
* Users can comment on watchlists as many times as they wish
* Rating, flagging, and commenting are irreversible
* A comment can be edited or remove by its creator if it has received no rating
* Watchlists can only be removed if they do not have any attached rating, comment, or tale
 
### 8.4 External Commands
* CreateWatchlist <!-- Creates a new watchlist --!>
* UpdateWatchlist <!-- Updates all details of a watchlist --!>
* RemoveWatchlist <!-- Removes a watchlist --!>
* AddWatchlistTag <!-- Adds a new tag to a watchlist --!>
* RemoveWatchlistTag <!-- Removes a tag from a watchlist --!>
* FlagWatchlist <!-- Report a watchlist for a violation --!>
* FollowWatchlist <!-- Follows a watchlist for notifications --!>
* UnfollowWatchlist <!-- Unfollows a watchlist --!>
* RateWatchlist <!-- Rates a watchlist --!>
* AddWatchlistComment <!-- Adds a new comment to a watchlist --!>
* AddWatchlistReply <!-- Adds a reply to a comment on a watchlist --!>
* RateWatchlistComment <!-- Rates a watchlist comment --!>
* FlagWatchlistComment <!-- Flags a watchlist comment --!>
* UpdateWatchlistComment <!-- Updates a watchlist comment --!>
* RemoveWatchlistComment <!-- Removes a watchlist comment --!>
* LinkTale <!-- Links a tale to watchlist --!>

### 8.5 CreateWatchlist
* Creates a new watchlist
* A user has to be registered to create a watchlist

#### 8.5.1 Request
* Title (string)
* Category (enum)
* Summary (string)
* SourceText (string)
* SourceUrl (string)
* Country (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.5.2 Response
* Id (Ulid) <!-- Id of newly created watchlist --!>
* CreatedAt (DateTime)

#### 8.5.3 Validation
* Title <!-- Not null. Not empty. 128. --!>
* Category <!-- Not null. IsInEnum. --!>
* Summary <!-- Not null. Not empty. 2096 --!>
* SourceText <!-- Not null. Not empty. 28 --!>
* SourceUrl <!-- Not null. Not empty. 255 --!>
* Country <!-- IsInEnum --!>

#### 8.5.4 Work flow
* Receives and validates request
* Slugifies title and creates a new watchlist
* Save changes
* Publish event: WatchlistCreated
* Returns response

### 8.6 UpdateWatchlist
* Updates a watchlist
* Created watchlists with any engagement i.e. rating, tale, comment can no longer be edited

#### 8.6.1 Request
* Id (Ulid)
* Title (string)
* Category (enum)
* Summary (string)
* SourceText (string)
* SourceUrl (string)
* Country (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.6.2 Response
* Ok

#### 8.6.3 Validation
* Id <!-- Not null. --!>
* Title <!-- Not null. Not empty. 128. --!>
* Category <!-- Not null. IsInEnum. --!>
* Summary <!-- Not null. Not empty. 2096 --!>
* SourceText <!-- Not null. Not empty. 28 --!>
* SourceUrl <!-- Not null. Not empty. 255 --!>
* Country <!-- IsInEnum --!>
* Category <!-- Not null. IsInEnum. --!>

#### 8.6.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception.
* Slugifies title and updates watchlist
* Save changes
* Publish event: WatchlistUpdated
* Returns response

### 8.7 AddWatchlistTag
* Adds a new tag to a watchlist

#### 8.7.1 Request
* Id (Ulid)
* Name (string) <!-- Name of tag --!>
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.7.2 Response
* Ok

#### 8.7.3 Validation
* Id <!-- Not null. --!>
* Name <!-- Not null. Not empty. 24. --!>

#### 8.7.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception.
* Calls an internal service to register tag and get its Id
* Creates a new WatchlistTag and attaches to watchlist
* Save changes
* Publish event: WatchlistUpdated, TagAdded
* Returns response

### 8.8 RemoveWatchlistTag
* Removes existing tag from a watchlist
* Watchlists with any engagement i.e. rating, tale, comment can no longer be edited

#### 8.8.1 Request
* Id (Ulid)
* TagId (Ulid) <!-- Id of tag --!>
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.8.2 Response
* Ok

#### 8.8.3 Validation
* Id <!-- Not null. --!>
* TagId <!-- Not null. --!>

#### 8.8.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception.
* Gets Tag by TagId. If null, throw exception
* Detaches WatchlistTag
* Save changes
* Publish event: WatchlistUpdated and TagRemoved
* Returns response

### 8.9 RemoveWatchlist
* Removes a watchlist
* Watchlists with any engagement i.e. rating, tale, comment cannot be removed

#### 8.9.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.9.2 Response
* Ok

#### 8.9.3 Validation
* Id <!-- Not null. --!>

#### 8.9.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception.
* Removes Watchlist
* Save changes
* Publish event: WatchlistRemoved
* Returns response

### 8.10 RateWatchlist
* Rates a watchlist with a pre-defined type
* Any registered user is allowed to rate a watchlist
* A registered user can only rate a specific watchlist once i.e. duplicates are not allowed
* This process is irreversible

#### 8.10.1 Request
* Id (Ulid)
* RateType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.10.2 Response
* Ok

#### 8.10.3 Validation
* Id <!-- Not null. --!>
* RateType <!-- Not null. IsInEnum. --!>

#### 8.10.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception.
* Creates WatchlistRate and attaches to watchlist
* Save changes
* Publish event: WatchlistRated
* Returns response

### 8.11 FlagWatchlist
* Flags a watchlist with a pre-defined type
* Any registered user is allowed to flag a watchlist
* A registered user can only flag a specific watchlist once i.e. duplicates are not allowed
* This process is irreversible

#### 8.11.1 Request
* Id (Ulid)
* FlagType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.11.2 Response
* Ok

#### 8.11.3 Validation
* Id <!-- Not null. --!>
* FlagType <!-- Not null. IsInEnum. --!>

#### 8.11.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception.
* Creates a WatchlistFlag and attaches to watchlist
* Save changes
* Publish event: WatchlistFlagged
* Returns response

### 8.12 FollowWatchlist
* Follows a watchlist for notifications on activities involving the insight
* A registered user can only follow a watchlist once i.e. duplicates are not allowed
* This process is reversible

#### 8.12.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 8.12.2 Response
* Ok

#### 8.12.3 Validation
* Id <!-- Not null. --!>

#### 8.12.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception
* Creates a WatchlistFollow and attaches to watchlist
* Save changes
* Publish event: WatchlistFollowed
* Returns response

### 8.13 UnfollowWatchlist
* Unfollows a watchlist
  
#### 8.13.1 Request
* Id (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 8.13.2 Response
* Ok

#### 8.13.3 Validation
* Id <!-- Not null. --!>

#### 8.13.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception
* Gets the WatchlistFollow attached to watchlist. If null, throw exception
* Detaches WatchlistFollow
* Save changes
* Publish event: WatchlistUnfollowed
* Returns response

### 8.14 AddWatchlistComment
* Adds a comment to a watchlist
* Registered users can add as many comments as they wish to a watchlist

#### 8.14.1 Request
* Id (Ulid)
* Text (string)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 8.14.2 Response
* CommentId (Ulid)
* CreatedAt (DateTime)
* Text (string) <!-- After HtmlContent cleanup --!>

#### 8.14.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 8.14.4 Work flow
* Receives and validates request
* Gets Insight by Id. If null, throw exception
* Cleans text which contains Html content, create a WatchlistComment and attaches to insight
* Save changes
* Publish event: WatchlistCommented
* Returns response

### 8.15 AddWatchlistReply
* Adds a comment to a watchlist
* Registered users can add as many comments as they wish to a watchlist

#### 8.15.1 Request
* Id (Ulid)
* Text (string)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 8.15.2 Response
* CommentId (Ulid)
* CreatedAt (DateTime)
* Text (string) <!-- After HtmlContent cleanup --!>

#### 8.15.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 8.15.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Clean text which contains Html content, creates WatchlistComment and attaches to comment and watchlist
* Save changes
* Publish event: WatchlistCommentReplied
* Returns response

### 8.16 UpdateWatchlistComment
* Updates text of a comment 
* A comment with any rating can no longer be updated

#### 8.17.1 Request
* Id (Ulid)
* Text (string)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 8.17.2 Response
* Text (string) <!-- After HtmlContent cleanup --!>

#### 8.17.3 Validation
* Id <!-- Not null. --!>
* Text <!-- Not null. Not empty. 1024. --!>

#### 8.17.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Clean text which contains html content and updates comment
* Save changes
* Publish event: WatchlistCommentUpdated
* Returns response

### 8.18 RemoveInsightComment
* Removes an existing comment
* A comment with any rating or replies can no longer be removed except with admin privilege

#### 8.18.1 Request
* Id (Ulid)
* CommentId (Ulid)
* AccountId (Ulid) <!-- From HttpContext. --!>

#### 8.18.2 Response
* Ok

#### 8.18.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>

#### 8.18.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception
* Gets Comment by Id. If null, throw exception
* Detaches comment
* Save changes
* Publish event: WatchlistCommentRemoved
* Returns response

### 8.19 RateWatchlistComment
* Rates a watchlist comment with a pre-defined type
* Any registered user is allowed to rate a watchlist comment
* A registered user can only rate a specific watchlist comment once i.e. duplicates are not allowed
* This process is irreversible

#### 8.20.1 Request
* Id (Ulid)
* CommentId (Ulid)
* RateType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.20.2 Response
* Ok

#### 8.20.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>
* RateType <!-- Not null. IsInEnum. --!>

#### 8.20.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception
* Gets comment by Id. If null, throw exception
* Creates WatchlistCommentRate and attaches to comment
* Save changes
* Publish event: WatchlistCommentRated
* Returns response

### 8.21 FlagWatchlistComment
* Flags a watchlist comment with a pre-defined type
* Any registered user is allowed to flag a watchlist comment
* A registered user can only flag a specific watchlist comment once i.e. duplicates are not allowed
* This process is irreversible

#### 8.22.1 Request
* Id (Ulid)
* CommentId (Ulid)
* FlagType (enum)
* AccountId (Ulid) <!-- From HttpContext --!>

#### 8.22.2 Response
* Ok

#### 8.22.3 Validation
* Id <!-- Not null. --!>
* CommentId <!-- Not null. --!>
* FlagType <!-- Not null. IsInEnum. --!>

#### 8.22.4 Work flow
* Receives and validates request
* Gets Watchlist by Id. If null, throw exception.
* Gets a comment by Id. If null, throw exception
* Creates WatchlistCommentFlag and attaches to comment
* Save changes
* Publish event: WatchlistCommentFlagged
* Returns response







































































