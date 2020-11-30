using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiHost.Extensions
{
    public class EventTasks
    {
        public const EventTask UI = (EventTask)1;
        public const EventTask Business = (EventTask)2;
        public const EventTask DA = (EventTask)3;
    }

    public class Tags
    {
        public const EventTags MSSQL = (EventTags)1;
        public const EventTags SQLLIT = (EventTags)2;
        public const EventTags DB2 = (EventTags)3;
    }

    [EventSource(Name = "ddd_sqllit_db")]
    public sealed class DatabaseEventSource : EventSource
    {
        public static readonly DatabaseEventSource Instance = new DatabaseEventSource();
        private DatabaseEventSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat) { }

        [Event(1, Level = EventLevel.Informational, Keywords = EventKeywords.None, Opcode = EventOpcode.Info,
            Task = EventTasks.DA, Tags = Tags.SQLLIT, Version = 1, Message = "sql : type : {0}, command text : {1}")]
        public void OnCammandExecute(int commandType, string commandText)
        {
            if (IsEnabled(EventLevel.Informational, EventKeywords.All, EventChannel.Debug))
            {
                WriteEvent(1, commandType, commandText);
            }
        }

        [Event(2, Level = EventLevel.Informational, Keywords = EventKeywords.None, Opcode = EventOpcode.Info)]
        public void PayloadHad(Payload payload)
        {
            if (IsEnabled(EventLevel.Informational, EventKeywords.All, EventChannel.Debug))
            {
                WriteEvent(2, payload);
            }
        }

        [Event(3, Level = EventLevel.Informational, Keywords = EventKeywords.None, Opcode = EventOpcode.Info)]
        public void RegisterComplete()
        {
            if (IsEnabled(EventLevel.Informational, EventKeywords.All, EventChannel.Debug))
            {
                WriteEvent(3, "customer", " register compete");
            }
        }
    }

    public class DatabaseSourceListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "System.Threading.Tasks.TplEventSource")
            {
                EnableEvents(eventSource, EventLevel.Informational, (EventKeywords)0x08);
            }
            if (eventSource.Name == "ddd_sqllit_db")
            {
                EnableEvents(eventSource, EventLevel.LogAlways);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventSource.Name == "ddd_sqllit_db")
            {
                var tmpColor = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine(JsonConvert.SerializeObject(eventData));
                Console.BackgroundColor = tmpColor;
            }
        }
    }


    [EventData]
    public class Payload
    {
        public SubPayload Sub { get; set; }
        public IEnumerable<SubPayload> SubPayloads { get; set; }
        public IDictionary<Guid, SubPayload> KeyValuePairs { get; set; }
    }

    [EventData]
    public class SubPayload
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubPayload(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
