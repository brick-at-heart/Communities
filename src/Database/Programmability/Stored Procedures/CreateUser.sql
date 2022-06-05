CREATE PROCEDURE [dbo].[CreateUser]
(
	@id						BIGINT OUTPUT,
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

	INSERT INTO [dbo].[User]
	(
		[DisplayName],
		[Email],
		[NormalizedEmail],
		[EmailConfirmed],
		[GivenName],
		[SurName],
		[PhoneNumber],
		[PhoneNumberConfirmed],
		[StreetAddressLine1],
		[StreetAddressLine2],
		[City],
		[Region],
		[Country],
		[PostalCode],
		[DateOfBirth],
		[Created]
	)
	VALUES
	(
		@displayName,
		@email,
		@normalizedEmail,
		@emailConfirmed,
		@givenName,
		@surName,
		@phoneNumber,
		@phoneNumberConfirmed,
		@streetAddressLine1,
		@streetAddressLine2,
		@city,
		@region,
		@country,
		@postalCode,
		@dateOfBirth,
		GetUtcDate()
	);

	SET @id = SCOPE_IDENTITY();

END