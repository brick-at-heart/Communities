CREATE ROLE [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateCatalog] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateCommunity] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateEvent] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateEventSchedule] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateLogin] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateMembership] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateMembershipRole] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateRole] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[CreateUser] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteCatalog] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteCommunity] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteEvent] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteEventSchedule] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteLogin] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteMembership] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteMembershipRole] TO [WebApplication];
Go

GRANT EXECUTE ON [dbo].[DeleteRole] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[DeleteUser] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveCatalogsByCommunityId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveCommunitiesByUserId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveCommunitiesByJoinType] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveCommunityByCommunityId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveCommunityByFullName] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveCommunityByShortName] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveEventByCommunityId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveEventByEventId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveEventScheduleByCommunityId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveEventScheduleByEventId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveLoginByUserId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveMembershipByMembershipId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveMembershipRolesByRoleId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveMembershipsByCommunityId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveMembershipsByUserId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveRightByRightIdUserId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveRightsByRoleId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveRoleByRoleId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveRolesByCommunityId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveUserByEmail] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveUserByLogin] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveUserByUserId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[RetrieveUsersByCommunityId] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[UpdateCommunity] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[UpdateEvent] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[UpdateEventSchedule] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[UpdateMembership] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[UpdateRole] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[UpdateRoleRight] TO [WebApplication];
GO

GRANT EXECUTE ON [dbo].[UpdateUser] TO [WebApplication];
GO
