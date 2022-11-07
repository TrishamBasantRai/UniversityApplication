using BusinesLayer.Functions;
using BusinesLayer.Services;
using Business_Layer.Functions;
using Business_Layer.Services;
using DataAccessLayer.Common;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;

using Unity;

namespace UniversityApplication
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IDAL, DAL>();

            container.RegisterType<ICalculateScore, CalculateScore>();
            container.RegisterType<IValidateLogin, ValidateLogin>();
            container.RegisterType<IValidateRegister, ValidateRegister>();
            container.RegisterType<IValidateStudent, ValidateStudent>();


            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IGradeRepository, GradeRepository>();
            container.RegisterType<IStudentSummaryRepository, StudentSummaryRepository>();
            container.RegisterType<ISubjectRepository, SubjectRepository>();
            container.RegisterType<IResultRepository, ResultRepository>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<IApplicationRepository, ApplicationRepository>();

            container.RegisterType<IValidateLogin, ValidateLogin>();
            container.RegisterType<IValidateRegister, ValidateRegister>();
            container.RegisterType<IValidateStudent, ValidateStudent>();

            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IRegisterService, RegisterService>();
            container.RegisterType<IResultService, ResultService>();
            container.RegisterType<IStudentService, StudentService>();
        }
    }
}