using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace testWpf
{
    public class InformationNode : INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        //public List<InformationNode> UpperInforamtionNodes = new List<InformationNode> ();
        private string text;
        public string? Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        private InformationNode upperInforamtionNodes;
        public InformationNode UpperInforamtionNodes
        {
            get { return upperInforamtionNodes; }
            set
            {
                upperInforamtionNodes = value;
                OnPropertyChanged(nameof(UpperInforamtionNodes));
            }

        }
        private Guid? upperInforamtionNodesId;
        public Guid? UpperInforamtionNodesId
        {
            get { return upperInforamtionNodesId; }
            set
            {
                upperInforamtionNodesId = value;
                OnPropertyChanged(nameof(UpperInforamtionNodesId));

            }
        }
        //private ICollection<InformationNode> lowerinformationNodes = new List<InformationNode>();

        public event PropertyChangedEventHandler? PropertyChanged;
        //private ICollection<InformationNode> lowerInformationNode;
        //public ICollection<InformationNode> LowerInformationNodes
        //{
        //    get { return lowerInformationNode == null? new List<InformationNode>(): lowerInformationNode; }
        //    set { lowerInformationNode = value; OnPropertyChanged(nameof(LowerInformationNodes)); }


        //}
        public ObservableCollection<InformationNode> LowerInformationNodes
        {
            get;set;


        } = new ObservableCollection<InformationNode>();    

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
