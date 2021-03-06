﻿using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccManager.Models.BusinessModels.Account;
using AccManager.Common.Enums;

namespace AccManager.Models.ViewModels.Account
{
    public class RoleViewModel : ViewModelBase<Role>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }

        public RoleViewModel SetFrom(Role entity)
        {
            base.SetFromEntity(entity);
            
            if (entity.Permissions != null)
            {
                Permissions = entity.Permissions.Select(p => p.Permission).ToList();
            }

            return this;
        }

        public override Role UpdateEntity(Role entity)
        {
            entity = base.UpdateEntity(entity);

            entity.Permissions = Permissions.Select(p => new RolePermission()
            {
                RoleId = entity.Id,
                Permission = p
            })
            .ToList();

            return entity;
        }
    }
}
