# 📚 OutScribed Backend - Project Documentation

---

**Last Updated:** July 23, 2025
**Project Lead:** OBI
**Status:** In Development

This document serves as the central repository for the backend architecture, domain models, processes, and Read Model definitions for the Tale Publishing Platform.

---

## 1. Project Overview

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
* Database-Level Full Text Search //

### 1.3 Modules
* Onboarding : Concerned with user pre-registration
* Analysis : Concerned with Insights
* Discovery : Concerned with Watchlists
* Identity : Concerned with Account Management
* Publishing : Concerned with Tales
* Tagging : Concerned with tagging
* Jail : Concerned with rate limiting

---

## 2. Project/Folder Architexture

This section provides a summary of the project architecture

*src
**  modules : (Folder) Write models for the modules
	 - **Module Name** : (Folder)
	 -- OutScribed.Modules.**ModuleName**.Application
	 --- Features : (Folder)
	 ---- **Feature Name** : (Folder) e.g. AddComment
	 ----- **Feature Name**Endpoint : (Class) Uses FastEndpoint
	 ----- **Feature Name**Request : (Class) Incoming request
	 ----- **Feature Name**Response : (Class) Outgoing response
	 ----- **Feature Name**Validator : (Class) Uses FluentValidation 
	 ----- **Feature Name**Service : (Class) Work flow/ Execution
	 -- OutScribed.Modules.**ModuleName**.Domain
	 --- Models : (folder) AggregateRoot, entities, value objects
**   OutScribed.API : (Project) Starting point of the application
	 - Extentions : (Folder) Service extensions
**  OutScribed.Application.Queries : (Project) Read models
	 - DTOs : (Folder) Service extensions
	 -- **Module Name** : (Folder)
	 --- **DTO Name**
	 - Features : (Folder) Service extensions
	 -- **Module Name** : (Folder)
	 --- **Feature Name** : (Folder) e.g. GetComments
	 --- **Feature Name**Endpoint : (Class) Uses FastEndpoint
	 --- **Feature Name**Request : (Class) Incoming request
	 --- **Feature Name**Response : (Class) Outgoing response
	 --- **Feature Name**Validator : (Class) Uses FluentValidation 
	 --- **Feature Name**Service : (Class) Execution
**  OutScribed.Infrastructure : (Project) Database, Email services
	 - Persistence : (Folder) 
	 -- Reads : (Folder) 
	 --- Configurations : (Folder)
	 ---- ReadsDbContext : (Class)
	 ---- **Module Name** : (Folder)
	 ----- **DTO**Configuration : (Class)
	 --- EventConsumers
	 -- Writes : (Folder) 
	 --- **Module Name** : (Folder)
	 ---- **AggregateRoot**Configuration : (Class)
	 ---- **Module Name**DbContext : (Class)
**   OutScribed.SharedKernel : (Project) Abstract classes, Enums, General Exceptions, Utilities classes (e.g. slug generator)
	 - Abstract : (Folder) 
	 - Enums : (Folder) 
	 - Exceptions : (Folder) 
	 - Utilities : (Folder) 
	 -- IdGenerator : (Class) Generates ID for entities
	 -- HtmlContentProcessor : (Class) Cleans Html content before storage: Remove dangerous attributes, scripts styles, comments, empty tags
	 -- TagsSlugHelper : (Class) Generates a slug from a tag name. Converts to lowercase, Remove diacritics, Remove all non-alphanumerics except dash
	 -- UrlSlugHelper : (Class) Generates a custom URL slug by concatenating the blog's publication date (YYYY-MM-DD), the unique creator's username, and a 
								slugified version of the title. Uses hyphens (-) as separators, avoiding forward slashes (/).

*tests
---

## 3. Write Model: Onboarding

This section provides description of the onboarding work flows

### 3.1 Description
* Onboarding handles pre-registration for new users
* It verifies a user through a verification token
* After the pre-registration is completed, the onboarding entries are permanently deleted
** Note: Can/should email address be verified to be valid & active?

### 3.2 Models
* TempUser : AggregateRoot
** Id (Ulid)
** EmailAddress (string)
** CreatedAt (DateTime)
** LastUpdated (DateTime) : Monitors last time a token was resent or token verification attempt was made
** VerifiedAttempts (int) : Monitors number of verification attempts
** IpAddress (string)
** IsVerified (bool) : If true, token is verified
** LockUpdates (bool) : If true, no token resends or verifications
** LockUntil (DateTime) : Time until lockUpDates is lifted
** ResendsCounter (int) : Monitors token resends
** One-time-token : ValueObject
*** Token (int) : Generated
*** ExpiresAt (DateTime) : Date created + 10 minutes


### 3.3 Business rules
* There must be at least 60 seconds between resends of verification tokens
* There is a limit of 5 verification token resends within 10 minutes span
* There is a limit of 5 verification attempts within 10 minutes span
* Verification tokens cannot be sent to the same IpAddress within a 10 minutes span
* Life span of a verification token is 10 minutes


### 3.4 Commands Summary
* SendToken : Creates account, Sends generated token to user
* ResendToken : Regeneartes a token and sends to user
* VerifyToken : Verifies token on file


### 3.5 SendToken
* Creates a new temporary account, generates and returns a verification token

#### 3.5.1 Request
* EmailAddress (string): Email address from user
* IpAddress (string): From HttpRequest

#### 3.5.2 Response
* Id (Ulid): Id of TempUser

#### 3.5.3 Validation
* EmailAddress : Not null. Not empty. 255.

#### 3.5.4 Work flow
* Receives request and performs validation then forwards to service
* Checks if at least 3 TempUser has been created from same Ip Address in last 10 minutes. If true, it jails IpAddress and rejects the request.
* Checks if TempUser by email address already exists
** Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, rejects the request.
** Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends and reject request.
** Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5.
* Else system generates a new verification token and creates a new TempUser
* Schedules an email with verification token
* Returns response

### 3.6 ResendToken
* Generates and returns a verification token

#### 3.6.1 Request
* Id (Ulid) : From frontend
* IpAddress (string): From HttpRequest

#### 3.6.2 Response
* Ok

#### 3.6.3 Validation
* EmailAddress : Not null. Not empty. 255.

#### 3.6.4 Work flow
* Receives request
* Checks if at least 3 TempUser has been created from same Ip Address in last 10 minutes. If true, it jails IpAddress and rejects the request.
* Checks if TempUser exists with Id. If false, it rejects the request.
* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, rejects the request.
* Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends and reject request.
* Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5.
* Else generates a new verification token and creates a new TempUser
* Schedules an email with verification token
* Returns response

### 3.7 VerifyToken
* Verifies a verification token

#### 3.7.1 Request
* Id (Ulid): From frontend
* IpAddress (string): From HttpRequest

#### 3.7.2 Response
* Ok

#### 3.7.3 Validation
* Id : Not null. Not empty.

#### 3.7.4 Work flow
* Receives request
* Checks if TempUser exists with Id. If false, it rejects the request.
* Checks if TempUser is already verified and rejects the request.
* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, rejects the request.
* Checks if too many verification attempts i.e. LastUpdated is less than 10 minutes and VerifiedAttempts is 5. If true, rejects the request.
* System compares the verification token, if false, it increments the VerificationAttempts and returns error to frontend.
* Else marks the IsVerified as true.
* Returns request

## 4. Write Model: Identity

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
* Account  (AggregateRoot)
** Id (Ulid)
** EmailAddress (string)
** RegisteredAt (DateTime) : Timestamp of registration
** Username (string) : Unique public identifier
** OneTimeToken (ValueObject) : Holds token used in password reset e.g. Token, ExpireAt.
--> Token (int) : Generated
--> ExpiresAt (DateTime) : Date created + 10 minutes
** RefreshToken (ValueObject) : Holds token used in JWT authentication e.g. Token, ExpireAt.
--> Token (int) : Generated
--> ExpiresAt (DateTime) : Date created + 300 minutes
** Password (ValueObject) : Holds password info e.g. Hash, Salt.
--> Hash (string) : Generated from received password
--> Salt (string) : Generated
** Profile (Entity) : Holds profile info e.g. Title, Bio, Photo, IsAnnonymous, etc.
--> Title (string) : User's title. No distinction between first, middle, and last name to accomodate businesses, etc.
--> AttachedAt (DateTime) : Date profile was first attached
--> IsAnnonymous (bool) : If true, user's title, bio, photo, and contact details would not be publicly displayed
--> Bio (string) : Html content. Requires cleanup before storage
--> Photo (string) : Location of photo on file. Storage is on DigitalOcean Space using AW3 SDK
** Admin (Entity) Holds admin e.g. Type, IsActive etc.
--> RoleType (enum) : Type of admin e.g. SuperAdmin, ContentModerator, Publisher, etc.
--> AssignedAt (DateTime) : Timestamp role was assigned
--> IsActive (bool) : If true, role can perform admin activities. Once assigned, admin roles are not removed. ***Is this best policy***
--> LastUpdated (DateTime) : Last admin was deactivated/activated. Defaults to AssignedAt
** LoginHistory (Entity) Holds info about user's logins e.g. IpAddress, etc. Account can have 0 or multiple LoginHistory
--> LoggedAt (DateTime) : Timestamp of login
--> IpAddress (string) : IpAddress used in login
** Contact (Entity) Holds info about user's contacts e.g. ContactType, etc. Account can have 0 or multiple Contact.
--> CreatedAt (DateTime) : Timestamp of first creation
--> LastUpdated (DateTime) : Timestamp of last change. Defaults to CreatedAt
--> Text (string) : Contact text
--> ContactType (enum) : Users can only use pre-defined contact type e.g. Facebook, EmailAddress, etc.
** Notification (Entity) Holds info about user's activity logs e.g. NotificationType, etc. Account can have 0 or multiple Notification.
--> HappendedAt (DateTime) : Timestamp of activity
--> HasRead (bool) : If true, notification has been read
--> Text (string) : Details of notification
--> ContactType (enum) : Users can only use pre-defined contact type e.g. Facebook, EmailAddress, etc.
--> NotificationType (enum) : Content affected e.g. Account, Tale, etc.
** Writer (Entity) : Holds writer info e.g. Address, Country, etc.
--> Address (string) : Writer's address information
--> AppliedAt (DateTime) : Timestamp of application to become writer
--> ApprovedAt (DateTime) : Timestamp of approval to become writer
--> IsActive (bool) : If true, has privilege to publish tale
--> Country (enum) : Country of origin/location
--> Application (string) : Location of writer's application in Adobe PDF on file. Storage is on DigitalOcean Space using AW3 SDK

### 4.3 Business rules
* Duplicate email address and username are not allowed
* There must be at least 60 seconds between resends of verification tokens
* There is a limit of 5 verification token resends within 10 minutes span
* There is a limit of 5 verification attempts within 10 minutes span
* Verification tokens cannot be sent to the same IpAddress within a 10 minutes span
* Life span of a verification token is 10 minutes
* 
* 
* 

### 4.4 Commands Summary
* CreateAccount : Creates new account
* LoginUser : Verifes a user's credentials
* LogoutUser : Resets user's refreshToken, other logout activities.
* UpdateProfile : Updates user's title, bio, etc.
* UpdateContact : Create or update a user's contact
* SendToken : Generates and sends a password reset token
* ResendToken : Generates and resends a password reset token
* ResetPassword : Verifies a password reset token and changes user's password
* ChangePassword : Updates a user's password
* ApplyAsWriter : Attaches a new application to become a writer
* ApproveWriter : Approves a writer application
* UpdateWriter : Updates a writer's privilege
* AssignRole : Assigns a role to a user
* UpdateRole : Updates a user's role e.g. deactivate role or change role
*  

### 4.5 CreateAccount
* Creates a new main account. 
* This is second stage of user registration after pre-registration handled by Onboarding module
* This stage assumes the user's email address is verified

#### 4.5.1 Request
* Id (Ulid): Id of TempUser from onboarding. Attaches by frontend
* Username (string)
* Password (string)

#### 4.5.2 Response
* Ok

#### 4.5.3 Validation
* Username : Not null. Not empty. 20.
* Password : Not null. Not empty. 8.
* Id : Not null.

#### 4.5.4 Work flow
* Receives request
* Gets email address from TempUser with Id. If no record found, throws an exception
* 
* System returns Id
