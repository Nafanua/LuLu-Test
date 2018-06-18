using AutoMapper;
using LULUTest.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LULUTest.Startup))]
namespace LULUTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BookModel, BookViewModel>());
            ConfigureAuth(app);            
        }
    }
}
