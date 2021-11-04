using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLiteLivro.model;

namespace SQLiteLivro
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        /*public class Livro : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            
            

            private void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }*/

        private SQLiteAsyncConnection _dbContext;
        private Livro livroSelecionado;
        private ObservableCollection<Livro> _livros;
        private Boolean toque = false;
        public MainPage()
        {
            InitializeComponent();
            _dbContext = DependencyService.Get<lAcessoDB>().GetConnection();
        }


        //private void lstvUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    toque = true;
        //    usuarioSelecionado = e.SelectedItem as Usuario;
        //    txtNome.Text = usuarioSelecionado.Nome;
        //}

        protected async override void OnAppearing()
        {
            await _dbContext.CreateTableAsync<Livro>();
            var livros = await _dbContext.Table<Livro>().ToListAsync();
            _livros = new ObservableCollection<Livro>(livros);
            lstvUsuarios.ItemsSource = _livros;

            base.OnAppearing();
        }

        private async void IncluirClicked(object sender, System.EventArgs e)
        {
            toque = false;
            Livro livro = new Livro();
            livro.Titulo = txtTitulo.Text.Trim();
            livro.Autor = txtAutor.Text.Trim();
            livro.Editora = txtEditora.Text.Trim();
            livro.Ano = txtAno.Text.Trim();

            if (!string.IsNullOrEmpty(txtTitulo.Text))
            {
                await _dbContext.InsertAsync(livro);
                _livros.Add(livro);
                txtTitulo.Text = "";
                txtAutor.Text = "";
                txtEditora.Text = "";
                txtAno.Text = "";
            }
            else
            {
                await DisplayAlert("Aviso", "Preencha o campo Titulo", "Ok");
                txtTitulo.Focus();
            }

        }

        private async void AlterarClicked(object sender, System.EventArgs e)
        {
            if (toque == true)
            {
                livroSelecionado.Titulo = txtTitulo.Text.Trim();
                if (!string.IsNullOrEmpty(txtTitulo.Text))
                {
                    await _dbContext.UpdateAsync(livroSelecionado);
                    txtTitulo.Text = "";
                    toque = false;
                }
                else
                {
                    await DisplayAlert("Aviso", "Preencha o campo Nome", "Ok");
                    toque = false;
                }

            }
            else
            {
                await DisplayAlert("Aviso", "Selecione um nome da lista", "Ok");
            }

        }

        private async void ExcluirClicked(object sender, System.EventArgs e)
        {
            if (toque == true)
            {
                if (!string.IsNullOrEmpty(txtTitulo.Text))
                {
                    await _dbContext.DeleteAsync(livroSelecionado);
                    _livros.Remove(livroSelecionado);
                    txtTitulo.Text = "";
                    toque = false;
                }
                else
                {
                    await DisplayAlert("Aviso", "Selecione um nome da lista", "Ok");
                    toque = false;
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Selecione um nome da lista", "Ok");
            }

        }

        private void lstvUsuarios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            toque = true;
            livroSelecionado = e.Item as Livro;
            txtTitulo.Text = livroSelecionado.Titulo;

        }
    }
}
