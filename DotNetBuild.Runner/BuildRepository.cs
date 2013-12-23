using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetBuild.Runner
{
    public interface IBuildRepository
    {
        Build GetById(Guid id);
        void Add(Build build);
    }

    public class BuildRepository : IBuildRepository
    {
        private static IList<Build> _store;

        public BuildRepository()
        {
            _store = new List<Build>();
        }

        public IEnumerable<Build> Store
        {
            get { return _store; }
        }

        public Build GetById(Guid id)
        {
            var build = _store.SingleOrDefault(b => b.Id == id);
            return build;
        }

        public void Add(Build build)
        {
            if (build == null) 
                throw new ArgumentNullException("build");

            _store.Add(build);
        }
    }
}