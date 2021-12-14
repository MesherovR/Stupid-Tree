using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace testWpf
{
    public class ViewModel : INotifyPropertyChanged
    {
        RelayCommand addCommand;
        RelayCommand saveCommand;
        RelayCommand deleteCommand;
        public ViewModel()
        {
            InformationNode virtualInformationNode = new InformationNode();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Nodes.ToList().ForEach(e =>
                { 
                    if(e.UpperInforamtionNodes == null)
                    {
                        virtualInformationNode.LowerInformationNodes.Add(e);
                    }                
                });
                InformationNode = virtualInformationNode;
            }

            
        }
        private InformationNode informationNode;
        public InformationNode InformationNode 
        { 
            get { return informationNode; } 
            set 
            {
                informationNode = value; 
                OnPropertyChanged(nameof(InformationNode)); 
            } 
        }

        private ObservableCollection<InformationNode> informationNodes;
        public ObservableCollection<InformationNode> InformationNodes 
        { 
            get { return InformationNode.LowerInformationNodes; }
            set
            {
                informationNode.LowerInformationNodes = value; OnPropertyChanged(nameof(InformationNodes)); 
            }
        }
        private InformationNode selectedInforamtionNode;
        public InformationNode SelectedInformationNode 
        { get { return selectedInforamtionNode; } set { selectedInforamtionNode = value; OnPropertyChanged(nameof(SelectedInformationNode)); } }


        
        private string inputText;
        public string InputText { get { return inputText; } set { inputText = value;OnPropertyChanged(nameof(InputText)); } }
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      if (InformationNode != null)
                      {
                         
                          using (ApplicationContext db = new ApplicationContext()) 
                          {
                              InformationNode informationNode = new InformationNode();
                              informationNode.Text = InputText;
                              InformationNode.LowerInformationNodes.Add(informationNode);
                              var d = db.Nodes.ToList().Find(e => e.Id == InformationNode.Id);
                              informationNode.UpperInforamtionNodes = d;
                              db.Nodes.Add(informationNode);
                              db.SaveChanges();
                          }
                      }
                  }));
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand((o) =>
                  {
                      using (ApplicationContext db = new ApplicationContext())
                      {
                          db.Update(InformationNode);
                          db.SaveChanges();
                      }
                  }));
            } 
        }



        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(obj =>
                    {
                        if(obj != null)
                        {
                            InformationNode informationNode = obj as InformationNode;
                            InformationNode = informationNode;
                        }
                        


                    }));
            }
        }


        private RelayCommand getAutoAnswer;
        public RelayCommand GetAutoAnswer
        {
            get
            {
                return getAutoAnswer ??
                    (getAutoAnswer = new RelayCommand(obj =>
                    {
                        string fullPath = "";
                        while (InformationNode.LowerInformationNodes.Count > 0)
                        {
                            InformationNode = InformationNode.LowerInformationNodes[new Random().Next(InformationNode.LowerInformationNodes.Count())];
                            fullPath = fullPath + "/" + InformationNode.Text  ;
                        }
                        
                        MessageBox.Show(fullPath);


                    }));
            }
        }

        private RelayCommand upCommand;
        public RelayCommand UpCommand
        {
            get
            {
                return upCommand ??
                    (upCommand = new RelayCommand(obj =>
                    {
                        if (InformationNode != null)
                        {


                            if (InformationNode.UpperInforamtionNodes != null )
                                InformationNode = InformationNode.UpperInforamtionNodes;
                        }

                    }));
            }
        }

        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      using(ApplicationContext db = new ApplicationContext())
                      {
                          InformationNode informationNode = selectedItem as InformationNode;
                          if (informationNode != null)
                          {
                              db.Nodes.Remove(informationNode);
                              db.SaveChanges();
                          }
                      }
                      
                      
                  }));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
