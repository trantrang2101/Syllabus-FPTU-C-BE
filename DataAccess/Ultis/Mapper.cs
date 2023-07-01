using AutoMapper;

namespace DataAccess.Ultis
{
    public class Mapper:Profile
    {
        public Mapper() {}

        protected internal Mapper(string profileName) : base(profileName) { }
    }
}
