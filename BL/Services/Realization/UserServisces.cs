using BL.DtoModels;
using BL.DtoModels.Combined;
using BL.Services.Abstract;
using BL.Services.Abstract.ValidationInterfaces;
using DL.Entities;
using DL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class UserServisces : IUserServices
    {
        private IUserEntityRepo _usersRepository;
        
        private IWorkerEntityRepo _workersRepository;
        
        private IRoleEntityRepository _roleRepository;
        
        private IUserRoleRepository _userRoleRepository;

        private IFullUserValidator _fullUserValidator;

        public UserServisces(IUserEntityRepo usersRepository, IWorkerEntityRepo workersRepository, IRoleEntityRepository roleRepository,
            IUserRoleRepository userRoleRepository, IFullUserValidator fullUserValidator)
        {
            _roleRepository = roleRepository;

            _usersRepository = usersRepository;

            _workersRepository = workersRepository;

            _userRoleRepository = userRoleRepository;

            _fullUserValidator = fullUserValidator;
        }

        public void Create(FullUser fullUser)
        {
            _fullUserValidator.CheckForValidyToCreate(fullUser);

            try
            {
                _workersRepository.Create(new WorkerEntity
                {
                    PassportNumber = fullUser.Worker.PassportNumber,
                    PersonalData = fullUser.Worker.PersonalData,
                });
            }
            catch (Exception)
            {
                throw;
            }
            if (fullUser.User != null)
            {
                int userId = _usersRepository.Create(new UserEntity { Id = fullUser.User.Id, Login = fullUser.User.Login, Password = fullUser.User.Password, WorkerId = fullUser.User.WorkerId });

                foreach (var item in fullUser.Roles)
                {
                    _userRoleRepository.Create(new UserRoleEntity() { UserId = userId, RoleId = item.Id });
                }
            }
        }

        public void Delete(FullUser fullUser)
        {
            _workersRepository.Delete(fullUser.Worker.PassportNumber);
        }

        public FullUser Read(string login, string password)
        {
            if (login == null || password == null)
            {
                login = "";
                password = "";
            }

            var users = _usersRepository.Read();
            var userEntity = users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();

            if (userEntity == null)
            {
                throw new ArgumentException("Wrong login or password");
            }

            var user = new User
            {
                Id = userEntity.Id,
                Login = userEntity.Login,
                Password = userEntity.Password,
                WorkerId = userEntity.WorkerId,
            };

            return GetFullUser(user);
        }

        public FullUser Read(int workerNumber)
        {

            var userEntity = _usersRepository.Read().Where(x => x.WorkerId == workerNumber).FirstOrDefault();

            User user;

            if (userEntity != null)
            {
                user = new User
                {
                    Id = userEntity.Id,
                    Login = userEntity.Login,
                    Password = userEntity.Password,
                    WorkerId = userEntity.WorkerId,
                };
            }
            else
            {
                user = new User
                {
                    WorkerId = workerNumber,
                };
            }

            return GetFullUser(user);
        }

        public void Update(FullUser fullUser)
        {
            _fullUserValidator.CkeckForValidyToUpdate(fullUser);

            _workersRepository.Update(new WorkerEntity { PassportNumber = fullUser.Worker.PassportNumber, PersonalData = fullUser.Worker.PersonalData });

            _usersRepository.Delete(_usersRepository.Read().First(x => x.WorkerId == fullUser.Worker.PassportNumber).Id);

            if (fullUser.User != null)
            {
                int userId = _usersRepository.Create(new UserEntity { Login = fullUser.User.Login, Password = fullUser.User.Password, WorkerId = fullUser.User.WorkerId, Id = fullUser.User.Id });

                int length = fullUser.Roles.Count;

                for (int i = 0; i < length; i++)
                {
                    var userRole = new UserRoleEntity() { RoleId = fullUser.Roles[i].Id, UserId = userId };

                    _userRoleRepository.Create(userRole);
                }
            }
        }

        private FullUser GetFullUser(User user)
        {
            var worker = _workersRepository.ReadById(user.WorkerId);

            FullUser result;

            if (user.Id != 0)
            {
                var userRoles = _userRoleRepository.Read().Where(x => x.UserId == user.Id);

                List<Role> roles = new List<Role>();

                foreach (var item in userRoles)
                {
                    var roleEntity = _roleRepository.ReadById(item.RoleId);

                    roles.Add(new Role { Description = roleEntity.Description, Title = roleEntity.Title, Id = roleEntity.Id });
                }

                result = new FullUser { User = user, Worker = new Worker { PassportNumber = worker.PassportNumber, PersonalData = worker.PersonalData }, Roles = roles };
            }
            else
            {
                result = new FullUser { User = new User { }, Worker = new Worker { PassportNumber = worker.PassportNumber, PersonalData = worker.PersonalData }, Roles = new List<Role> { } };
            }

            return result;
        }
    }
}
