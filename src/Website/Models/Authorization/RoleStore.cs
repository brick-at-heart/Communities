using BrickAtHeart.Communities.Data;
using BrickAtHeart.Communities.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Models.Authorization
{
    public class RoleStore : IRoleStore<Role>
    {
        public RoleStore( IRoleDataClient roleDataClient,
                          ILookupNormalizer normalizer,
                          ILogger<RoleStore> logger )
        {
            this.roleDataClient = roleDataClient;
            this.normalizer = normalizer;
            this.logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered CreateAsync");

            IRoleEntity roleEntity = LoadEntity(role);

            try
            {
                await roleDataClient.CreateRoleAsync(roleEntity, cancellationToken);

                role.Id = roleEntity.Id;
                role.Name = roleEntity.Name;
                role.NormalizedName = roleEntity.NormalizedName;
                role.CommunityId = role.CommunityId;
                role.IsDefault = role.IsDefault;

                logger.LogInformation("Successfully Leaving CreateAsync");

                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in CreateAsync {e.HResult}, {e.Message}");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        public async Task<IdentityResult> CreateRoleMembershipAsync(RoleMembership roleMembership, CancellationToken cancellationToken = new())
        {
            IMembershipRoleEntity membershipRoleEntity = LoadEntity(roleMembership);

            try
            {
                await roleDataClient.CreateMembershipRoleAsync(membershipRoleEntity, cancellationToken);

                roleMembership.Id = membershipRoleEntity.Id;
                roleMembership.MembershipId = membershipRoleEntity.MembershipId;
                roleMembership.RoleId = membershipRoleEntity.RoleId;
                //roleMembership.DisplayName = membershipRoleEntity.DisplayName;
                //roleMembership.GivenName = membershipRoleEntity.GivenName;
                //roleMembership.SurName = membershipRoleEntity.SurName;

                logger.LogInformation("Successfully Leaving CreateRoleMembershipAsync");

                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in CreateRoleMembershipAsync {e.HResult}, {e.Message}");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteAsync");

            IRoleEntity roleEntity = LoadEntity(role);

            try
            {
                await roleDataClient.DeleteRoleAsync(roleEntity, cancellationToken);

                logger.LogInformation("Successfully Leaving DeleteAsync");

                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in DeleteAsync {e.HResult}, {e.Message}");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        public async Task<IdentityResult> DeleteRoleMembershipAsync(RoleMembership roleMembership, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered DeleteRoleMembershipAsync");

            IMembershipRoleEntity membershipRoleEntity = LoadEntity(roleMembership);

            try
            {
                await roleDataClient.DeleteMembershipRoleAsync(membershipRoleEntity, cancellationToken);

                logger.LogInformation("Successfully Leaving DeleteRoleMembershipAsync");

                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in DeleteRoleMembershipAsync {e.HResult}, {e.Message}");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        public void Dispose()
        {
        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered FindByIdAsync");

            IRoleEntity? roleEntity;

            try
            {
                roleEntity = await roleDataClient.RetrieveRoleByRoleIdAsync(long.Parse(roleId), cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in FindByIdAsync {e.HResult}, {e.Message}");
                throw;
            }

            if (roleEntity == null)
            {
                throw new ApplicationException();
            }

            Role role = LoadModel(roleEntity);

            logger.LogInformation("Successfully Leaving FindByIdAsync");

            return role;
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken = new())
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = new())
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = new())
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = new())
        {
            return Task.FromResult(role.Name);
        }

        public async Task<IList<RoleRight>> RetrieveRightsByRoleIdAsync(long roleId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveRightsByRoleIdAsync");

            try
            {
                IList<IRightEntity> rightEntites = await roleDataClient.RetrieveRightsByRoleIdAsync(roleId, cancellationToken);
                IList<RoleRight> rights = LoadModels(rightEntites);

                logger.LogInformation("Successfully leaving RetrieveRightsByRoleIdAsync");

                return rights;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Errir in RetrieveRightsByRoleIdAsync {ex.HResult}, {ex.Message}");
                IList<RoleRight> rights = new List<RoleRight>();

                return rights;
            }
        }

        public async Task<IList<RoleRight>> RetrieveRightByRightIdUserIdAsync(long rightId, long userId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveRightByRightIdUserIdAsync");

            try
            {
                IList<IRightEntity> rightEntites = await roleDataClient.RetrieveRightByRightIdUserIdAsync(rightId, userId, cancellationToken);
                IList<RoleRight> rights = LoadModels(rightEntites);

                logger.LogInformation("Successfully leaving RetrieveRightByRightIdUserIdAsync");

                return rights;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Errir in RetrieveRightByRightIdUserIdAsync {ex.HResult}, {ex.Message}");
                IList<RoleRight> rights = new List<RoleRight>();

                return rights;
            }
        }

        public async Task<IList<RoleMembership>> RetrieveRoleMembershipByRoleIdAsync(long roleId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveRoleMembershipByRoleIdAsync");

            try
            {
                IList<IMembershipRoleEntity> membershipRoleEntities = await roleDataClient.RetrieveMembershipRolesByRoleIdAsync(roleId, cancellationToken);
                IList<RoleMembership> roleMemberships = LoadModels(membershipRoleEntities);

                logger.LogInformation("Successfully leaving RetrieveRoleMembershipByRoleIdAsync");

                return roleMemberships;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Error in RetrieveRoleMembershipByRoleIdAsync {ex.HResult}, {ex.Message}");
                throw;
            }
        }

        public async Task<IList<Role>> RetrieveRolesByUserGroupIdAsync(long userGroupId, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered RetrieveRolesByUserGroupIdAsync");

            try
            {
                IList<IRoleEntity> roleEntity = await roleDataClient.RetrieveRolesByUserGroupIdAsync(userGroupId, cancellationToken);
                IList<Role> roles = LoadModels(roleEntity);

                logger.LogInformation("Successfully Leaving RetrieveRolesByUserGroupIdAsync");

                return roles;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in RetrieveRolesByUserGroupIdAsync {e.HResult}, {e.Message}");
                throw;
            }
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken = new())
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken = new())
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered UpdateAsync");

            IRoleEntity roleEntity = LoadEntity(role);

            try
            {
                await roleDataClient.UpdateRoleAsync(roleEntity, cancellationToken);

                logger.LogInformation("Successfully Leaving UpdateAsync");

                return IdentityResult.Success;
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in UpdateAsync {e.HResult}, {e.Message}");

                IdentityError[] errs =
                {
                    new ()
                    {
                        Code = e.HResult.ToString(),
                        Description = e.Message
                    }
                };

                return IdentityResult.Failed(errs);
            }
        }

        public async Task UpdateRoleRightAsync(RoleRight right, CancellationToken cancellationToken = new())
        {
            logger.LogInformation("Entered UpdateRoleRightAsync");

            IRightEntity rightEntity = LoadEntity(right);

            try
            {
                await roleDataClient.UpdateRoleRightAsync(rightEntity, cancellationToken);

                logger.LogInformation("Successfully leaving UpdateRoleRight");
            }
            catch (Exception e)
            {
                logger.LogWarning(e, $"Error in UpdateRoleRightAsync {e.HResult}, {e.Message}");
                throw;
            }
        }

        private IMembershipRoleEntity LoadEntity(RoleMembership roleMembership)
        {
            return new MembershipRoleEntity(roleMembership.DisplayName, roleMembership.GivenName, roleMembership.SurName)
            {
                Id = roleMembership.Id,
                MembershipId = roleMembership.MembershipId,
                RoleId = roleMembership.RoleId
            };
        }

        private IRightEntity LoadEntity(RoleRight right)
        {
            return new RightEntity(right.Name)
            {
                Id = right.Id,
                State = right.State.ToNullableBool()
            };
        }

        private IRoleEntity LoadEntity(Role role)
        {
            return new RoleEntity(role.Name, role.NormalizedName)
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = normalizer.NormalizeName(role.NormalizedName),
                CommunityId = role.CommunityId,
                IsDefault = role.IsDefault
            };
        }

        private RoleRight LoadModel(IRightEntity rightEntity)
        {
            return new RoleRight(rightEntity.Name)
            {
                Id = rightEntity.Id,
                State = rightEntity.State.ToRightState()
            };
        }

        private Role LoadModel(IRoleEntity roleEntity)
        {
            return new Role
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
                NormalizedName = roleEntity.NormalizedName,
                CommunityId = roleEntity.CommunityId,
                IsDefault = roleEntity.IsDefault
            };
        }

        private RoleMembership LoadModel(IMembershipRoleEntity membershipRoleEntity)
        {
            return new RoleMembership(membershipRoleEntity.DisplayName, membershipRoleEntity.GivenName, membershipRoleEntity.SurName)
            {
                Id = membershipRoleEntity.Id,
                MembershipId = membershipRoleEntity.MembershipId,
                RoleId = membershipRoleEntity.RoleId
            };
        }

        private IList<RoleRight> LoadModels(IList<IRightEntity> rightEntities)
        {
            IList<RoleRight> rights = new List<RoleRight>();

            if (rightEntities != null)
            {
                foreach (IRightEntity rightEntity in rightEntities)
                {
                    rights.Add(LoadModel(rightEntity));
                }
            }

            return rights;
        }

        private IList<Role> LoadModels(IList<IRoleEntity> roleEntities)
        {
            IList<Role> roles = new List<Role>();

            if (roleEntities != null)
            {
                foreach (IRoleEntity roleEntity in roleEntities)
                {
                    roles.Add(LoadModel(roleEntity));
                }
            }

            return roles;
        }

        private IList<RoleMembership> LoadModels(IList<IMembershipRoleEntity> membershipRoleEntities)
        {
            IList<RoleMembership> roleMemberships = new List<RoleMembership>();

            if (membershipRoleEntities != null)
            {
                foreach (IMembershipRoleEntity membershipRoleEntity in membershipRoleEntities)
                {
                    roleMemberships.Add(LoadModel(membershipRoleEntity));
                }
            }

            return roleMemberships;
        }

        private readonly IRoleDataClient roleDataClient;
        private readonly ILogger<RoleStore> logger;
        private readonly ILookupNormalizer normalizer;
    }
}