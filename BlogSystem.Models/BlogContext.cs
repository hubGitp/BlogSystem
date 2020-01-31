namespace BlogSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class BlogContext : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“BlogContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“BlogSystem.Models.BlogContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“BlogContext”
        //连接字符串。
        public BlogContext()
            : base("name=con")
        {
            Database.SetInitializer<BlogContext>(strategy: null);

        }
        //关闭关联表的联级删除
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
        public  DbSet<User> Users { get; set; }
        public DbSet<BlogCagetory> BlogCagetories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleToCategory> ArticleToCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Fans> Fans { get; set; }
    }
}
