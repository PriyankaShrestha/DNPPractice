using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.DataContainer;

namespace Blazor.Data
{
    public interface IVolumeService
    {
        Task<VolumeResult> CalculateVolume(double height, double radius, string type);
        Task<IList<VolumeResult>> GetHistory();
    }
}