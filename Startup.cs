using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuanLyHocSinh.Startup))]
namespace QuanLyHocSinh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
