﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSJCDecaux
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IJCDecaux
    {
        [OperationContract]
        Station[] GetStations(string contract, int timeout);

        [OperationContract]
        string[] GetContracts();
    }
}
