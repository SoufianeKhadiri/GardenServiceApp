using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Garten.Model
{
    public class EventAggregator
    {
    }
    public class SendMyPosts : PubSubEvent<ObservableCollection<Post>> { }

    public class TakeFoto : PubSubEvent<bool> { }
    public class TakeFotoGalery : PubSubEvent<bool> { }
}
