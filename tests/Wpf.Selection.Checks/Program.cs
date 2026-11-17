using ReactiveUI;
using ReactiveUI.Builder;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using ServiceLib.Common;
using ServiceLib.Manager;
using ServiceLib.Models.Dto;
using v2rayN.Views;
class Program {
 [STAThread] static void Main() {
  Console.WriteLine("Creating application");
  var app = new v2rayN.App(); Console.WriteLine("Loading resources"); app.InitializeComponent(); Console.WriteLine("Initializing isolated config");
  if (!AppManager.Instance.InitApp()) throw new Exception("Test config initialization failed");
  RxAppBuilder.CreateReactiveUIBuilder().WithWpf().BuildApp();
  Console.WriteLine("Creating profiles view");
  var view = new ProfilesView();
  var grid = (DataGrid)view.FindName("lstProfiles");
  var us = new ProfileItemModel { IndexId="us", IpInfo="(US) 1.1.1.1" };
  var de = new ProfileItemModel { IndexId="de", IpInfo="(DE) 1.1.1.1" };
  var source = new ObservableCollection<ProfileItemModel> { us, de };
  grid.ItemsSource=source;
  var collectionView=CollectionViewSource.GetDefaultView(source);
  var flags=BindingFlags.Instance|BindingFlags.NonPublic;
  var country=typeof(ProfilesView).GetField("_country",flags)!;
  typeof(ProfilesView).GetField("_countryView",flags)!.SetValue(view,collectionView);
  collectionView.Filter=p=> (string)country.GetValue(view)! =="" || ProfileCountry.GetCode(((ProfileItemModel)p).IpInfo,null)==(string)country.GetValue(view)!;
  var apply=typeof(ProfilesView).GetMethod("ApplyCountryFilter",flags)!;
  grid.SelectedItems.Add(us); grid.SelectedItems.Add(de);
  apply.Invoke(view,new object[]{"DE"});
  if(grid.Items.Count!=1 || !grid.SelectedItems.Contains(de)) throw new Exception("DE selection lost");
  apply.Invoke(view,new object[]{""});
  if(grid.SelectedItems.Count!=2) throw new Exception("Hidden selections not restored");
  apply.Invoke(view,new object[]{"US"});
  grid.SelectedItems.Clear();
  apply.Invoke(view,new object[]{""});
  if(grid.SelectedItems.Count!=1 || !grid.SelectedItems.Contains(de)) throw new Exception("Explicit deselection not preserved");
  Console.WriteLine("PASS: production WPF filter selection restoration and deselection");
 }
}
