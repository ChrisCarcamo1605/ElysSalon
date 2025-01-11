using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ElySalon.infra.Domain
{
    internal class ArticleShow
    {

        public void addArticle()
        {
            var contexto = new elysalondbEntities();
           
            decimal precioCompra = 12.25m;
            var article = new article
            {
                article_id = 1,
                article_name = "MESSSII",
                article_type_id = 1,
                price_buy = precioCompra,
                price_cost = 15.75m,
                stock = 2,
                description = "description"
            };
            contexto.article.Add(article);
            contexto.SaveChanges();

        }



       

       



    }
}
