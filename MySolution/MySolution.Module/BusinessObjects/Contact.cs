using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;

namespace MySolution.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Contact : Person
    {
        private DateTime anniversary;
        private string nickName;
        private string notes;
        private string spouseName;
        private TitleOfCourtesy titleOfCourtesy;
        private string webPageAddress;

        public Contact(Session session) : base(session) { }

        public DateTime Anniversary
        {
            get { return anniversary; }
            set { SetPropertyValue("Anniversary", ref anniversary, value); }
        }
        public string NickName
        {
            get { return nickName; }
            set { SetPropertyValue("NickName", ref nickName, value); }
        }
        [Size(4096)]
        public string Notes
        {
            get { return notes; }
            set { SetPropertyValue("Notes", ref notes, value); }
        }
        public string SpouseName
        {
            get { return spouseName; }
            set { SetPropertyValue("SpouseName", ref spouseName, value); }
        }
        public TitleOfCourtesy TitleOfCourtesy
        {
            get { return titleOfCourtesy; }
            set { SetPropertyValue("TitleOfCourtesy", ref titleOfCourtesy, value); }
        }
        public string WebPageAddress
        {
            get { return webPageAddress; }
            set { SetPropertyValue("WebPageAddress", ref webPageAddress, value); }
        }
        private Department department;
        [Association("Department-Contacts")]
        public Department Department
        {
            get { return department; }
            set { SetPropertyValue("Department", ref department, value); }
        }
        private Position position;
        public Position Position
        {
            get { return position; }
            set { SetPropertyValue("Position", ref position, value); }
        }

        [Association("Contact-DemoTask")]
        public XPCollection<DemoTask> Tasks
        {
            get { return GetCollection<DemoTask>("Tasks"); }
        }

        private Contact manager;
        [DataSourceProperty("Department.Contacts", DataSourcePropertyIsNullMode.SelectAll)]
        [DataSourceCriteria("Position.Title = 'Manager' AND Oid != '@This.Oid'")]
        public Contact Manager
        {
            get { return manager; }
            set { SetPropertyValue("Manager", ref manager, value); }
        }

    }
    public enum TitleOfCourtesy { Dr, Miss, Mr, Mrs, Ms };

    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty("Title")]
    public class Department : BaseObject {
        public Department(Session session) : base(session){ }
        private string title;
        public string Priority
        {
            get { return title; }
            set { SetPropertyValue("Title", ref title, value); }
        }

        private string office;
        public string Office
        {
            get { return office; }
            set { SetPropertyValue("Office", ref office, value); }
        }
        [Association("Department-Contacts")]
        public XPCollection<Contact> Contacts
        {
            get { return GetCollection<Contact>("Contacts"); }
        }
    }
    [DefaultClassOptions]
    [System.ComponentModel.DefaultProperty("Title")]
    public class Position : BaseObject
    {
        public Position(Session session) : base(session) { }
        private string title;
        [RuleRequiredField(DefaultContexts.Save)]
        public string Title
        {
            get { return title; }
            set { SetPropertyValue("Title", ref title, value); }
        }
    }
    [DefaultClassOptions]
    [ModelDefault("Caption", "Task")]
    public class DemoTask : Task
    {
        public DemoTask (Session session):base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Priority = Priority.Normal;
        }

        [Association("Contact-DemoTask")]
        public XPCollection<Contact> Contacts
        {
            get { return GetCollection<Contact>("Contacts"); }
        }
        private Priority priority;
        public Priority Priority
        {
            get { return priority; }
            set { SetPropertyValue("Priority", ref priority, value); }
        }
    }

    public enum Priority
    {
        Low = 0,
        Normal = 1,
        High = 2
    }


}
    