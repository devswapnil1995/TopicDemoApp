using System;
using System.Collections.Generic;
using System.Text;

namespace TopicDemoApp
{
    public interface ITopicModule
    {
        string Name { get; }
        string Description { get; }
        void Run();
    }
}
