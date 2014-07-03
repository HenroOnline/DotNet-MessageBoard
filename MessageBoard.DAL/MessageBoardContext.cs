using MessageBoard.DAL.Configuration;
using MessageBoard.DAL.Entity;
using MessageBoard.DAL.Initializer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.DAL
{
	public class MessageBoardContext : DbContext
	{
		internal static string SchemaName
		{
			get;
			set;
		}

		public DbSet<Board> Boards { get; set; }
		public DbSet<Slide> Slides { get; set; }
		public DbSet<Layer> Layers { get; set; }
		public DbSet<BoardSlide> BoardSlides { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<InformationText> InformationTexts { get; set; }
		public DbSet<Setting> Settings { get; set; }

		private static MessageBoardContext _instance;
		public static MessageBoardContext Instance
		{
			get
			{
				if (System.Web.HttpContext.Current != null)
				{
					string key = typeof(MessageBoardContext).ToString();

					if (System.Web.HttpContext.Current.Items[key] == null)
					{
						System.Web.HttpContext.Current.Items[key] = CreateContext();
					}

					return (MessageBoardContext)System.Web.HttpContext.Current.Items[key];
				}
				else
				{
					if (_instance == null)
					{
						_instance = CreateContext();
					}
					return _instance;
				}
			}
		}

		protected static MessageBoardContext CreateContext()
		{
			var result = new MessageBoardContext();
			Database.SetInitializer<MessageBoardContext>(new MessageBoardInitializer());
			return result;
		}

		public MessageBoardContext()
		{
			if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SchemaName"]))
			{
				MessageBoardContext.SchemaName = ConfigurationManager.AppSettings["SchemaName"];
			}

			this.Configuration.LazyLoadingEnabled = true;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			modelBuilder.Configurations.Add(new BoardConfiguration());			
			modelBuilder.Configurations.Add(new SlideConfiguration());
			modelBuilder.Configurations.Add(new BoardSlideConfiguration());
			modelBuilder.Configurations.Add(new MessageConfiguration());
			modelBuilder.Configurations.Add(new InformationTextConfiguration());
			modelBuilder.Configurations.Add(new SettingConfiguration());
		}

		public override int SaveChanges()
		{
			DateTime currentDate = DateTime.Now;
			var user = "MessageBoard";
			foreach (var addedEntity in ChangeTracker.Entries<Base>().Where(b => b.State == System.Data.Entity.EntityState.Added))
			{
				addedEntity.Entity.CreatedDate = currentDate;
				addedEntity.Entity.CreatedUser = user;
				addedEntity.Entity.ModifiedKind = "I";

				addedEntity.Entity.ModifiedDate = currentDate;
				addedEntity.Entity.ModifiedUser = user;
			}

			foreach (var entity in ChangeTracker.Entries<Base>().Where(b => b.State == System.Data.Entity.EntityState.Modified))
			{
				entity.Entity.ModifiedDate = currentDate;
				entity.Entity.ModifiedUser = user;

				if (entity.Entity.ModifiedKind != "D")
				{
					entity.Entity.ModifiedKind = "M";
				}
			}

			try
			{
				return base.SaveChanges();
			}
			catch (DbEntityValidationException dbEx)
			{
				foreach (var validationErrors in dbEx.EntityValidationErrors)
				{
					foreach (var validationError in validationErrors.ValidationErrors)
					{
						Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
					}
				}

				throw;
			}
		}
	}
}
