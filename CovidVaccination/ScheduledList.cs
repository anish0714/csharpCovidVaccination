using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections;



namespace CovidVaccination
{
    [XmlRoot("ScheduledList")]
    public class ScheduledList : IEnumerable<Schedule>
    {
        [XmlArray("ScheduleArray")]
        [XmlArrayItem("Schedule", typeof(Schedule))]

        private List<Schedule> scheduleListObj = null;

        public List<Schedule> ScheduleListObj { get => scheduleListObj; set => scheduleListObj = value; }

        public ScheduledList()
        {
            scheduleListObj = new List<Schedule>();
        }
        public Schedule this[int i]
        {
            get => this.scheduleListObj[i]; set => this.scheduleListObj[i] = value;
        }
        public IEnumerator<Schedule> GetEnumerator()
        {
            return ((IEnumerable<Schedule>)scheduleListObj).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Schedule>)scheduleListObj).GetEnumerator();
        }
        public int Count
        {
            get => this.scheduleListObj.Count;
        }
        public void Add(Schedule schObj)
        {
            scheduleListObj.Add(schObj);
        }

        public void Remove(Schedule schObj)
        {
            scheduleListObj.Remove(schObj);
        }

        
        public void ClearScheduleList()
        {
            scheduleListObj.Clear();
        } 
    }
}
