using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace internship_3_oop_intro
{
    public class Event
    {
        public Event(string name, EventType eventType, DateTime startTime, DateTime endTime)
        {
            Name = name;
            EventType0 = eventType;
            StartTime = startTime;
            EndTime = endTime;
        }

        public string Name { get; set; }
        public enum EventType
        {
            Coffee,
            Lecture,
            Concert,
            StudySession
        }
        public EventType EventType0 { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
