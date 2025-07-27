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
* Database-Level Full Text Search 

### 1.3 Modules
* Onboarding : Concerned with user pre-registration
* Analysis : Concerned with Insights
* Discovery : Concerned with Watchlists
* Identity : Concerned with Account Management
* Publishing : Concerned with Tales
* Tagging : Concerned with tagging
* Jail : Concerned with rate limiting

## 1.4. Project/Folder Architexture

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

## 1.5. Communication

* 

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
* Receives request
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
* Receives request and performs validation then forwards to service
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
* Id _Not null. Not empty._

#### 3.7.4 Work flow
* Receives request
* Checks if TempUser exists with Id. If false, throw new exception.
* Checks if TempUser is already verified, throw new exception.
* Checks if updates are locked and lock time has not expired i.e. LockUpdates is true and LastUpdated is less than 30 minutes. It true, throw new exception.
* Checks if it is less than 60 seconds since last token resent i.e. LastUpdated is less than 60 seconds. If true, lock resends, save changes and throw new exception.
* Checks if too many token resends in last 10 minutes i.e. LastUpdated is less than 10 minutes and ResendsCounter is 5. If true, lock resends, save changes, and throw new exception.
* Compare the verification token, if false, it increments the VerificationAttempts and returns error to frontend.
* Else marks the IsVerified as true.
* Returns request

---

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
* 

### 4.4 Commands Summary
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
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
* Receives request
* Gets Account with Id. If null, throw an exception.
* Checks if Admin entity is attached. If false, throws an exception.
* Update Admin.
* Save changes
* Returns request

