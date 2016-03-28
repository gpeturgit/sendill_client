using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace sendill_service.ServiceContracts
{
    interface ICarRestService
    {
        [OperationContract]
        List<dtoCar> FindAll();

        [OperationContract]
        bool CreateCar(dtoCar ocar);

        [OperationContract]
        bool RemoveCar(int pid);

        [OperationContract]
        bool UpdateCar(dtoCar ocar);
    }
}
