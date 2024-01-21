using AFPStore.Core;
using AFPStore.MVVM.Model.Main;
using AFPStore.MVVM.View.DialogViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AFPStore.MVVM.ViewModel
{
    class OptionsViewModel : ObservableObject
    {
        RelayCommand? addUserProfileCommand;
        RelayCommand? editUserProfileCommand;
        RelayCommand? deleteUserProfileCommand;
        RelayCommand? addRoleCommand;
        RelayCommand? editRoleCommand;
        RelayCommand? deleteRoleCommand;

        List<UserProfile> userProfiles;
        UserProfile selectedUserProfile;
        List<Role> roles;
        Role selectedRole;
        public List<UserProfile> UserProfiles
        {
            get => userProfiles;
            set
            {
                userProfiles = value;
                OnPropertyChanged("UserProfiles");
            }
        }
        public UserProfile SelectedUserProfile
        {
            get => selectedUserProfile;
            set
            {
                selectedUserProfile = value;
                OnPropertyChanged("SelectedUserProfile");
            }
        }
        public List<Role> Roles
        {
            get => roles;
            set
            {
                roles = value;
                OnPropertyChanged("Roles");
            }
        }
        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                OnPropertyChanged("SelectedRole");
            }
        }
        public OptionsViewModel()
        {
            using StoreDbContext db = new();
            UserProfiles = db.UserProfiles.Include(r=> r.Role).Include(u=>u.User).ToList();
            Roles = db.Roles.ToList();
        }

        public RelayCommand AddUserProfileCommand
        {
            get
            {
                return addUserProfileCommand ??= new RelayCommand(async (o) =>
                {
                    User user = new() { Login = string.Empty, Password = string.Empty };
                    UserProfile profile = new() { FirstName = string.Empty, LastName = string.Empty, User = user };
                    UserProfileView dialog = new(profile);
                    if (dialog.ShowDialog() == true)
                    {
                        using StoreDbContext db = new();
                        db.Users.Add(user);
                        await db.SaveChangesAsync();
                        profile.UserId = db.Entry(user).Entity.Id;
                        db.UserProfiles.Add(profile);
                        await db.SaveChangesAsync();
                        CustomMessageBoxWithOnlyOKView msg = new($"Создан новый пользователь {profile.LastName} {profile.FirstName}");
                        msg.ShowDialog();
                        UserProfiles = db.UserProfiles.Include(r => r.Role).Include(u => u.User).ToList();
                    }
                });
            }
        }

        public RelayCommand AddRoleCommand
        {
            get
            {
                return addRoleCommand ??= new RelayCommand(async (o) =>
                {
                    Role role = new() { Name = string.Empty };
                    RoleView dialog = new(role);
                    if (dialog.ShowDialog() == true)
                    {
                        using StoreDbContext db = new();
                        db.Roles.Add(role);
                        await db.SaveChangesAsync();
                        CustomMessageBoxWithOnlyOKView msg = new($"Создана новая роль {role.Name}");
                        msg.ShowDialog();
                        Roles = db.Roles.ToList();
                    }
                });
            }
        }
        public RelayCommand EditUserProfileCommand
        {
            get
            {
                return editUserProfileCommand ??= new RelayCommand(async (selectedItem) =>
                {
                    // получаем выделенный объект
                    UserProfile? userProfile = selectedItem as UserProfile;
                    if (userProfile == null) return;

                    UserProfileView dialog = new(userProfile);


                    if (dialog.ShowDialog() == true)
                    {
                        using StoreDbContext db = new();
                        db.Entry(userProfile).State = EntityState.Modified;
                        db.Entry(userProfile.User).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        UserProfiles = db.UserProfiles.Include(r => r.Role).Include(u => u.User).ToList();
                    }
                });
            }
        }
        public RelayCommand EditRoleCommand
        {
            get
            {
                return editRoleCommand ??= new RelayCommand(async (selectedItem) =>
                {
                    // получаем выделенный объект
                    Role? role = selectedItem as Role;
                    if (role == null) return;

                    RoleView dialog = new(role);


                    if (dialog.ShowDialog() == true)
                    {
                        using StoreDbContext db = new();
                        db.Entry(role).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        Roles = db.Roles.ToList();
                        UserProfiles = db.UserProfiles.Include(r => r.Role).Include(u => u.User).ToList();
                    }
                });
            }
        }
        public RelayCommand DeleteUserProfileCommand
        {
            get
            {
                return deleteUserProfileCommand ??= new RelayCommand(async (selectedItem) =>
                {
                    using StoreDbContext db = new();
                    // получаем выделенный объект
                    if (selectedItem is not UserProfile userProfile) return;
                    //Точно удалить?
                    CustomMessageBoxView dialog = new($"Вы точно хотите удалить пользователя {userProfile.LastName} {userProfile.FirstName}?");
                    if (dialog.ShowDialog() == true)
                    {
                        db.Users.Remove(userProfile.User);
                        db.UserProfiles.Remove(userProfile);
                        await db.SaveChangesAsync();
                    }
                    UserProfiles = db.UserProfiles.Include(r => r.Role).Include(u => u.User).ToList();
                });
            }
        }
        public RelayCommand DeleteRoleCommand
        {
            get
            {
                return deleteRoleCommand ??= new RelayCommand(async (selectedItem) =>
                {
                    using StoreDbContext db = new();
                    // получаем выделенный объект
                    if (selectedItem is not Role role) return;
                    //Точно удалить?
                    CustomMessageBoxView dialog = new($"Вы точно хотите удалить роль {role.Name}?");
                    if (dialog.ShowDialog() == true)
                    {
                        if(!db.UserProfiles.Where(o=>o.RoleId == role.Id).Any())
                        {
                            db.Roles.Remove(role);
                            await db.SaveChangesAsync();
                            Roles = db.Roles.ToList();
                            UserProfiles = db.UserProfiles.Include(r => r.Role).Include(u => u.User).ToList();
                        }
                        else
                        {
                            CustomMessageBoxWithOnlyOKView msg = new("Нельзя удалить роль, которая привязана хотя бы к одному пользователю!");
                            msg.ShowDialog();
                        }
                    }
                });
            }
        }
    }
}
