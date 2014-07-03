using MessageBoard.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.BLL.Repositories
{
	public class LayerRepository : BaseRepository<Layer, LayerRepository>
	{
		public List<Layer> ListBySlide(int slideId)
		{
			return Context.Layers.Where(l => l.ModifiedKind != "D")
													 .Where(l => l.Slide.Id == slideId)
													 .OrderBy(l => l.Sequence).ToList();
		}

		public override void Delete(Layer entity)
		{
			foreach (var message in MessageRepository.Instance.ListByLayer(entity.Id))
			{
				MessageRepository.Instance.Delete(message);
			}
			base.Delete(entity);
		}
	}
}
