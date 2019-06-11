using JBHiFi.ProductManagement.Business.CommandQueryHandlers;
using JBHiFi.ProductManagement.Business.Interfaces;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace JBHiFi.ProductManagement.Business
{
    public class ApiBusinessRegistry:Registry
    {

        public ApiBusinessRegistry()
        {
            this.Scan(p =>
            {
                p.TheCallingAssembly();
                p.ConnectImplementationsToTypesClosing(typeof(IQueryFor<,>));
                p.ConnectImplementationsToTypesClosing(typeof(IQueryAll<>));
                p.ConnectImplementationsToTypesClosing(typeof(ICommand<>));
                p.SingleImplementationsOfInterface();
                p.WithDefaultConventions();
            });
            For<IQueryHandler>().Use<QueryHandler>();
            For<ICommandHandler>().Use<CommandHandler>();

        }
    }
}
