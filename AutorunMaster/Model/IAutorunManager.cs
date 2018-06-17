using System.Collections.Generic;
using AutorunMaster.Model.Entities;

namespace AutorunMaster.Model
{
    public interface IAutorunManager
    {
        List<AutorunObject> GetAutorunObjects();
    }
}
