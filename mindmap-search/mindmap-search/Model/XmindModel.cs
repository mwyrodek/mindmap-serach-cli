using System.Collections.Generic;

namespace mindmap_search.Model
{
    public class Attached
    {
        public string id { get; set; }
        public string title { get; set; }
        public Children children { get; set; }
    }

    public class Children
    {
        public List<Attached> attached { get; set; }
    }

    public class Content
    {
        public string content { get; set; }
        public string name { get; set; }
    }

    public class Extension
    {
        public List<Content> content { get; set; }
        public string provider { get; set; }
    }

    public class RootTopic
    {
        public string id { get; set; }
        public string @class { get; set; }
        public string title { get; set; }
        public string structureClass { get; set; }
        public Children children { get; set; }
        public List<Extension> extensions { get; set; }
    }


    public class XmindModel
    {
        public string title { get; set; }
        public RootTopic rootTopic { get; set; }
    }
}



