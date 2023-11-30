using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{

    public interface IBundleRepository
    {
        Bundle GetById(int bundleId);
        void Update(Bundle bundle);
        //Bundle GetBundleByTourId(int tourId);
        List<Bundle> GetBundlesByAuthorId(int userId);
    }

}
