using AutoMapper;
using PL.Infrastructure.Services.Abstract;
using PL.Models.ModelsForView;
namespace PL.Infrastructure.Services
{
    public class FullUserServisces : IFullUserServices
    {
        private BL.Services.Abstract.IUserServices _userServices;
        private Mapper _mapper;
        public FullUserServisces(BL.Services.Abstract.IUserServices userServices, Mapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public void Create(FullUser fullUser)
        {
            _userServices.Create(_mapper.Map<FullUser, BL.DtoModels.Combined.FullUser>(fullUser));
        }

        public void Delete(FullUser fullUser)
        {
            _userServices.Delete(_mapper.Map<FullUser, BL.DtoModels.Combined.FullUser>(fullUser));
        }

        public FullUser Read(string login, string password)
        {
            FullUser result = _mapper.Map<BL.DtoModels.Combined.FullUser, FullUser>(_userServices.Read(login, password));
            return result;
        }

        public FullUser Read(int workerNumber)
        {
            FullUser result = _mapper.Map<BL.DtoModels.Combined.FullUser, FullUser>(_userServices.Read(workerNumber));
            return result;
        }

        public void Update(FullUser fullUser)
        {
            _userServices.Update(_mapper.Map<FullUser, BL.DtoModels.Combined.FullUser>(fullUser));
        }
    }
}
