function formatar(mascara, documento) {
    var i = documento.value.length;
    var saida = mascara.substring(0, 1);
    var texto = mascara.substring(i)

    if (texto.substring(0, 1) != saida) {
        documento.value += texto.substring(0, 1);
    }

}

function getCargo() {
    $.ajax({
        method: 'GET',
        url: '/cliente/getCargo',
        success: function (result) {
            
        }
    });
 };

   
function clean() {
    document.getElementById('name').value = '';
    document.getElementById('lastName').value = '';
    document.getElementById('cpf').value = '';
    document.getElementById('calendario').value = '';
    refresh();
};

function refresh() {
    var test = "";
    $.ajax({
        method: 'GET',
        url: '/cliente/getCliente/',
        success: function (result) {
            var string = "<tr> <th>Nome</th> <th>Sobrenome</th> <th>Cpf</th> <th>Data Nascimento</th> <th>Idade</th> <th>Profissao</th><th>Alterar</th><th>Excluir</th></tr >";
            for (var i = 0; i <= (result.length - 1); i++) {
                var dados = result[i];
                var nome = dados.name.replace('+', '');
                string += " <tr > <td>" + nome.replace('+', '') + "</td><td>" + dados.lastName + "</td> <td>" + dados.cpf +
                    " </ td > <td>" + dados.date +
                    "</td> <td>" + dados.age +
                    "</td > <td>" + dados.profession +
                    "</td> <td> <button type='button' class='btn btn - primary' onclick='alterar("+dados.id+")'> Alterar </button>" +
                    "</td><td>  <button type='button' class='btn btn - primary' onclick='excluir(" + dados.id + ")'>Excluir </button>" +
                    "</td></tr>"
            }
            test = string;

            document.getElementById("tabela").innerHTML = string;
            document.getElementById('btnSaveAlter').innerHTML = 'Adicionar';
        }
    });
};


function save() {
    var name = document.getElementById('name').value;
    var lastname = document.getElementById('lastName').value;
    var cpf = document.getElementById('cpf').value;
    var prof = document.getElementById('prof').value;
    var date = document.getElementById('calendario').value;
    if (!name == "" && !lastname == "" && !cpf == "" && !age == "" && !date == ''){
        if (TestaCPF(cpf)) {
            if (document.getElementById("btnSaveAlter").textContent == "Adicionar") {
                $.ajax({
                    method: 'GET',
                    url: '/cliente/saveCliente/' + name + '/' + lastname + '/' + cpf + '/' + prof + '/' + date,
                    success: function (result) {
                        alert("Cliente cadastrado com sucesso!")
                        clean();
                    }
                });
                refresh();
            } else {
                var id = document.getElementById('btnSaveAlter').value;
                $.ajax({
                    method: 'GET',
                    url: '/cliente/alterarCliente/' + id + '/' + name + '/' + lastname + '/' + cpf + '/' + prof + '/' + date,
                    success: function (result) {
                        clean();
                    }
                });
                clean();
                refresh();
            }
        } else {
            alert("CPF Invalido");
        }
    } else {
        alert("Prencha todos os dados");
    }
    refresh();
};


//Cálculo Data de nascimento/Idade
document.getElementById("calendario").addEventListener('change', function () {
    var data = new Date(this.value);
    if (isDate_(this.value) && data.getFullYear() > 1900)
        document.getElementById("age").value = calculateAge(this.value);
});

function calculateAge(dobString) {
    var dob = new Date(dobString);
    var currentDate = new Date();
    var currentYear = currentDate.getFullYear();
    var birthdayThisYear = new Date(currentYear, dob.getMonth(), dob.getDate());
    var age = currentYear - dob.getFullYear();
    if (birthdayThisYear > currentDate) {
        age--;
    }
    return age;
}

function calcular(calendario) {
    var calendario = document.form.nascimento.value;
    alert(calendario);
    var partes = data.split("/");
    var junta = partes[2] + "-" + partes[1] + "-" + partes[0];
    document.form.idade.value = (calculateAge(junta));
}

var isDate_ = function (input) {
    var status = false;
    if (!input || input.length <= 0) {
        status = false;
    } else {
        var result = new Date(input);
        if (result == 'Invalid Date') {
            status = false;
        } else {
            status = true;
        }
    }
    return status;
}

$(function () {
    $("#calendario").datepicker();
   
    refresh();
});

function excluir(id) {
    $.ajax({
        method: 'GET',
        url: '/cliente/excluir/' +id,
        success: function (result) {
            alert("Excluído com sucesso!")
            refresh();
        }
    });
    refresh();
};

function alterar(id) {
    $.ajax({
        method: 'GET',
        url: '/cliente/obterCliente/' + id,
        success: function (result) {
            var dados = result[0];
            document.getElementById('name').value = dados.name;
            document.getElementById('lastName').value = dados.lastName;
            document.getElementById('cpf').value = dados.cpf;
           // document.getElementById("prof").options[dados.profession].selected = "true";
           // document.getElementById('calendario').value = dados.date;
            document.getElementById('age').value = dados.age;
        }
    });
    document.getElementById('btnSaveAlter').innerHTML = 'Alterar';
    document.getElementById('btnSaveAlter').value = id;
};

function TestaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;
    strCPF = strCPF.replace('-', '');
    strCPF = strCPF.replace('.', '');
    strCPF = strCPF.replace('.', '');
    if (strCPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) return false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) return false;
    return true;
}

