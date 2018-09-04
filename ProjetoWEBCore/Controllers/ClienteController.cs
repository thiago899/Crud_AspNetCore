using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoWEBCore.Model;

namespace ProjetoWEBCore.Controllers
{
    [Route("cliente")]
    public class ClienteController : Controller
    {
        [Route("saveCliente/{name}/{lastname}/{cpf}/{prof}/{dia}/{mes}/{ano}")]
        public async Task<object> SaveUser(String name, String lastname, String cpf, String prof, String dia, String mes, String ano)
        {
            int dat = DateTime.Now.Year;
            int age = (dat-Convert.ToInt32(ano));
             dbCoreContext db = new dbCoreContext();
            String data = dia + "-" + mes + "-" + ano;
            DateTime MyDateTime = DateTime.ParseExact(data, "dd-MM-yyyy", null);
            cpf = cpf.Replace("-", "");
            cpf = cpf.Replace(".", "");
            cpf = cpf.Replace(".", "");
            using (var context = new dbCoreContext())
            {
                var Cli= new Cliente()
                {
                    nome = name,
                    sobrenome = lastname,
                    cpf = cpf.Trim(),
                    idade = age,
                    profissao_id = Convert.ToInt16(prof),
                    data_nascimento = MyDateTime
                };

                context.clientes.Add(Cli);
                context.SaveChanges();

            }
            return true;
        }

        [Route("getCargo")]
        public async Task<object> getCargo()
        {
             dbCoreContext db = new dbCoreContext();
            var listaClientes = db.profissao;

            var professions = (from p in db.profissao
                               select p);
            return professions;
        }

        [Route("getCliente")]
        public async Task<object> getClientes()
        {
            dbCoreContext db = new dbCoreContext();
            List<ModelCliente> listaClientesVM = new List<ModelCliente>();

            //consulta os dados do banco de dados
            //fazendo a junção da duas tabelas
            //e armazenando as informações em listaClientes
          
            var model = (from c in db.clientes
                               join p in db.profissao 
                               on c.profissao_id equals p.Id
                               select new
                               {
                                   c.id,
                                   c.nome,
                                   c.sobrenome,
                                   c.cpf,
                                   c.data_nascimento,
                                   c.idade,
                                   p.cargo
                               }).ToList();
            foreach (var item in model)
            {
                ModelCliente cliVM = new ModelCliente(); //ViewModel
                cliVM.id = item.id;
                cliVM.name = item.nome;
                cliVM.lastName = item.sobrenome;
                cliVM.cpf = item.cpf;
                cliVM.date = item.data_nascimento;
                cliVM.age = item.idade;
                cliVM.profession = item.cargo;
                listaClientesVM.Add(cliVM);
            }

            return listaClientesVM;
        }

        [Route("excluir/{id}")]
        public async Task<object> Excluir(int id)
        {
            dbCoreContext db = new dbCoreContext();
            using (var context = new dbCoreContext())
            {
                const string query = "DELETE FROM [dbo].[clientes] WHERE [id]={0}";
                var rows = context.Database.ExecuteSqlCommand(query, id);
            }
            return true;
        }

        [Route("obterCliente/{id}")]
        public async Task<object> obterClientes(int id)
        {
            dbCoreContext db = new dbCoreContext();
            List<ModelCliente> listaClientesVM = new List<ModelCliente>();

            //consulta os dados do banco de dados
            //fazendo a junção da duas tabelas
            //e armazenando as informações em listaClientes

            var model = (from c in db.clientes
                         join p in db.profissao
                         on c.profissao_id equals p.Id
                         where c.id == id 
                         select new
                         {
                             c.id,
                             c.nome,
                             c.sobrenome,
                             c.cpf,
                             c.data_nascimento,
                             c.idade,
                             p.cargo
                         }).ToList();
            foreach (var item in model)
            {
                ModelCliente cliVM = new ModelCliente(); //ViewModel
                cliVM.id = item.id;
                cliVM.name = item.nome;
                cliVM.lastName = item.sobrenome;
                cliVM.cpf = item.cpf;
                cliVM.date = item.data_nascimento;
                cliVM.age = item.idade;
                cliVM.profession = item.cargo;
                listaClientesVM.Add(cliVM);
            }

            return listaClientesVM;
        }

        [Route("alterarCliente/{id}/{name}/{lastname}/{cpf}/{prof}/{dia}/{mes}/{ano}")]
        public async Task<object> AlterarUser(int id,String name, String lastname, String cpf, String prof, String dia, String mes, String ano)
        {
            int dat = DateTime.Now.Year;
            int age = (dat - Convert.ToInt32(ano));
            dbCoreContext db = new dbCoreContext();
            String data = dia + "-" + mes + "-" + ano;
            DateTime MyDateTime = DateTime.ParseExact(data, "dd-MM-yyyy", null);

                using (var context = new dbCoreContext())
                {
                     string query = "UPDATE [dbo].[clientes] SET[nome] = '"+name+
                                          "',[sobrenome] = '" + lastname +
                                          "',[cpf] = " + cpf +
                                          ",[data_nascimento] = '" + MyDateTime +
                                          "',[idade] = " + age +
                                          ",[profissao_id] = " + Convert.ToInt16(prof) +
                                          " WHERE [id]={0}";
                    var rows = context.Database.ExecuteSqlCommand(query, id);
                }
            return true;
        }
    }
}
