using BL.DtoModels.Combined;
using BL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
namespace BL.Services
{
    class OperationServices : IOperationServices
    {
        private IServiceServices _serviceServices;
        private IComponetServices _componentServices;
        private IBuildStandartServices _serviceComponetServices;
        public OperationServices(IServiceServices serviceServices, IComponetServices componentServices, IBuildStandartServices serviceComponetServices)
        {
            _serviceServices = serviceServices;
            _componentServices = componentServices;
            _serviceComponetServices = serviceComponetServices;
        }
        public void Create(BuildStandart operation)
        {
            throw new NotImplementedException();
        }

        public void Delete(BuildStandart operation)
        {
            
        }

        public List<BuildStandart> Read(int componentId = -1, int serviceId = -1)
        {
            //List<> _serviceComponetServices.Read(minComponetId: componentId, maxComponetId: componentId, minServiceId:serviceId,maxServiceId:serviceId);
            return null;
        }

        public void Update(BuildStandart operation)
        {
            throw new NotImplementedException();
        }
    }
}
