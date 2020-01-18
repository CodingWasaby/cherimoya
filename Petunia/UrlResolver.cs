namespace Petunia
{
    class UrlResolver
    {
        public UrlResolver(string rootUrl)
        {
            this.rootUrl = rootUrl;
        }


        private string rootUrl;


        public string GetPlanUrl(string id)
        {
            return rootUrl + string.Format("/Repository/Plans/{0}.txt", id);
        }
    }
}
