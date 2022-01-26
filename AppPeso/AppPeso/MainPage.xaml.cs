using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppPeso
{
    public partial class MainPage : ContentPage

    {
        Service.ServicesCliente ObjClienteService;
        string altura;
        string peso;
        string nombre="";
        string apellido="";
        string IMC = "";
        public MainPage()
        {
            InitializeComponent();
            ObjClienteService = new Service.ServicesCliente();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string mensaje = await ObjClienteService.registrarUser(txtnombre.Text, txtapellido.Text, txtaltura.Text, txtpeso.Text);
            JObject json = JObject.Parse(mensaje);
            limpiarErrorCampos();
            foreach (var datos in json)
            {
                var b = datos.ToString().Trim(new char[] { '[', ']' });
                string[] a = b.Split(',');
               
                switch (a[0])
                {
                    case "nombre":

                        lblmensajenombre.Text = a[1];
                        break;
                    case "apellido":

                        lblmensajeapellido.Text = a[1];
                        break;
                    case "peso":

                        lblmensajepeso.Text = a[1];
                        break;
                    case "altura":

                        lblmensajealtura.Text = a[1];
                        break;

                    case "success":
                       
                        nombre = a[1];
                        apellido = a[2];
                        altura = a[3];
                        peso = a[4];
                        IMC = a[5];
                        await DisplayAlert("Usuario", "Registrado..!", "OK");
                        limpiarErrorFields();
                        break;

                }
            }
        }

        private async void Button_ClickedReporte(object sender, EventArgs e)
        {
           
            if (!String.IsNullOrEmpty(nombre))
            {
                string []auxnombre = nombre.Split(':');
                string nameaux = auxnombre[1].Substring(1);
                string mensajenombre = nameaux.Substring(1);
                string Nombre = mensajenombre.Remove(mensajenombre.Length-1);

                string[] auxapellido = apellido.Split(':');
                string apellidoaux = auxapellido[1].Substring(1);
                string mensajeapellido = apellidoaux.Substring(1);
                string Apellido = mensajeapellido.Remove(mensajeapellido.Length-1);

                string[] auxapeso = peso.Split(':');
                string pesoaux = auxapeso[1].Substring(1);
                string mensajepeso = pesoaux.Substring(1);
                string Peso = mensajepeso.Remove(mensajepeso.Length-1);

                string[] auxaltura = altura.Split(':');
                string alturaaux = auxaltura[1].Substring(1);
                string mensajealtura = alturaaux.Substring(1);
                string Altura = mensajealtura.Remove(mensajealtura.Length-1);

           

                string mensaje = "Usuario : " + Nombre + " " + Apellido+"\nPeso : "+Peso+"\nAltura : "+ Altura;
                string calcul = calculos(Altura, Peso);
                await DisplayAlert(calcul,mensaje, "OK");


                
            }
            else
            {
                await DisplayAlert("Por favor","Ingrese los campos", "OK");
            }
            
        }


        public void limpiarErrorCampos()
        {
            lblmensajenombre.Text = "";
            lblmensajeapellido.Text = "";
            lblmensajealtura.Text = "";
            lblmensajepeso.Text = "";
           

        }


        public void limpiarErrorFields()
        {
            txtnombre.Text = "";
            txtapellido.Text = "";
           txtaltura.Text = "";
           txtpeso.Text = "";


        }
        public string calculos(string altura, string peso)
        {
            
            string a = altura.Replace('.', ',');
            double Altura = Double.Parse(a);
            double Peso = Double.Parse(peso);
            double imc = Peso/(Altura*Altura);
            string resultado = "";
            if (imc < 18.5)
            {
                resultado = "Tienes bajo peso";
            }
            else if (imc >= 18.5 && imc <= 24.9)
            {
                resultado = "Tu peso es normal";
            }
            else if (imc >= 25 && imc <= 29.9)
            {
                resultado = "Tienes sobre peso";
            }
            else
            {
                resultado = "Tienes obecidad, ¡Cuidade!";
            }

            return resultado+ "IMC : "+imc;
        }


        

    }
}
