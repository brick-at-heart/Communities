CREATE PROCEDURE [dbo].[UpdateUser]
(
	@id						BIGINT,
	@isActive				BIT,
	@isApproved				BIT,
	@displayName			NVARCHAR(100),
	@email					NVARCHAR(320),
	@normalizedEmail		NVARCHAR(320),
	@emailConfirmed			BIT,
	@givenName				NVARCHAR(100),
	@surName				NVARCHAR(100),
	@phoneNumber			NVARCHAR(24),
	@phoneNumberConfirmed	BIT,
	@streetAddressLine1		NVARCHAR(100),
	@streetAddressLine2		NVARCHAR(100),
	@city					NVARCHAR(100),
	@region					NVARCHAR(100),
	@country				NVARCHAR(100),
	@postalCode				NVARCHAR(100),
	@dateOfBirth			DATE
)
AS
BEGIN

	UPDATE [dbo].[User]
	SET	[IsActive] = @isActive,
		[IsApproved] = @isApproved,
		[DisplayName] = @displayName,
		[Email] = @email,
		[EmailConfirmed] = @emailConfirmed,
		[NormalizedEmail] = @normalizedEmail,
		[GivenName] = @givenName,
		[SurName] = @surName,
		[PhoneNumber] = @phoneNumber,
		[PhoneNumberConfirmed] = @phoneNumberConfirmed,
		[StreetAddressLine1] = @streetAddressLine1,
		[StreetAddressLine2] = @streetAddressLine2,
		[City] = @city,
		[Region] = @region,
		[Country] = @country,
		[PostalCode] = @postalCode,
		[DateOfBirth] = @dateOfBirth,
		[Updated] = GetUtcDate()
	WHERE [Id] = @id;

END