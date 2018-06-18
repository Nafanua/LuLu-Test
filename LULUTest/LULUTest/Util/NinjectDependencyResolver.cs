using LULUTest.Models;
using LULUTest.Models.Abstract;
using LULUTest.Util.Abstract;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using static LULUTest.Util.EmailService;

namespace LULUTest.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IBookReposytory>().To<BookRepository>();
            _kernel.Bind<IEmailService>().To<EmailService>().WithConstructorArgument("config", new SmtpConfig()
            {
                EmailAddress = ConfigurationManager.AppSettings["emailAdress"],
                Host = ConfigurationManager.AppSettings["smtpHost"],
                Name = ConfigurationManager.AppSettings["senderName"],
                Password = ConfigurationManager.AppSettings["password"],
                Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]),
                Username = ConfigurationManager.AppSettings["userName"],
                UseSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["useSsl"])
            }
                );
            _kernel.Bind<IOrderProcessor>().To<OrderProcessor>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}