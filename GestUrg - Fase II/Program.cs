using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GestUrg___Fase_II
{

    public class Pessoa
    {
        public int NumFuncionario;
        public string Nome;
        public string Telefone;
        public string Email;

        public Pessoa(int _numFuncionario, string _nome, string _telefone, string _email)
        {
            NumFuncionario = _numFuncionario;
            Nome = _nome;
            Telefone = _telefone;
            Email = _email;
        }
    }
    public class Medico : Pessoa
    {
        public string Especialidade;
        public Utente EmAtendimento = null; // Médico em atendimento quando recebe um objecto da classe Utente
        public string Estado;

        public Medico(int _numFuncionario, string _nome, string _telefone, string _email, string _especialidade, string _estado, Utente _ematendimento) : base(_numFuncionario, _nome, _telefone, _email)
        {
            Especialidade = _especialidade;
            EmAtendimento = _ematendimento;
            Estado = _estado;
        }
    }
    public class TecnicoAtendimento : Pessoa
    {
        public string Estado;

        public TecnicoAtendimento(int _numFuncionario, string _nome, string _telefone, string _email, string _estado) : base(_numFuncionario, _nome, _telefone, _email)
        {
            Estado = _estado;
        }
    }
    public class Utente
    {
        public int NumUtente;
        public string Nome;
        public string Telefone;
        public string Email;

        public Utente(int _numUtente, string _nome, string _telefone, string _email)
        {
            NumUtente = _numUtente;
            Nome = _nome;
            Telefone = _telefone;
            Email = _email;
        }
    }
    public class Urgencia
    {
        public int NumUrg;
        public DateTime DataHora { get; set; }
        public string Relatorio;
        public int NumFuncionarioMedico;
        public int NumUtenteSaude;
        public static int UrgenciaCounter = 0;

        public Urgencia(int _numUrg, DateTime _dataHora, string _relatorio, int _numFuncionarioMedico, int _numUtenteSaude)
        {
            NumUrg = _numUrg;
            DataHora = _dataHora;
            Relatorio = _relatorio;
            NumFuncionarioMedico = _numFuncionarioMedico;
            NumUtenteSaude = _numUtenteSaude;
            ++UrgenciaCounter;
        }
    }
    public class Tree
    {
        private class Node
        {
            internal Node lChild;
            internal Node rChild;
            internal string value1;

            public Node(string v, Node l, Node r)
            {
                value1 = v;
                lChild = l;
                rChild = r;
            }
            public Node(string v)
            {
                value1 = v;
                lChild = null;
                rChild = null;
            }
        }
        private Node root;

        public Tree()
        {
            root = null;
        }

        public void InsertNode(string value)
        {
            root = InsertNode(value, root);
        }
        private Node InsertNode(string value, Node node)
        {
            if (node == null)
            {
                node = new Node(value, null, null);
            }
            else
            {
                if (node.value1.CompareTo(value) > 0)
                {
                    node.lChild = InsertNode(value, node.lChild);
                }
                else
                {
                    node.rChild = InsertNode(value, node.rChild);
                }
            }
            return node;
        }

        public void PrintInOrder()
        {
            PrintInOrder(root);
        }
        private void PrintInOrder(Node node) //   In-order
        {
            if (node != null)
            {
                PrintInOrder(node.lChild);
                Console.Write("\n " + node.value1);
                PrintInOrder(node.rChild);
            }
        }

        public string FindMax()	//   Procura o maior
        {
            Node node = root;
            if (node == null)
            {
                return null;
            }

            while (node.rChild != null)
            {
                node = node.rChild;
            }
            return node.value1;

        }
        public bool Find(string value) //   Procura valor
        {
            Node curr = root;

            while (curr != null)
            {
                if (curr.value1 == value)
                {
                    return true;
                }
                else if (curr.value1.CompareTo(value) > 0)
                {
                    curr = curr.lChild;
                }
                else
                {
                    curr = curr.rChild;
                }
            }
            return false;
        }

        public void DeleteNode(string value) //  Elimina um nó com valor
        {
            root = DeleteNode(root, value);
        }
        private Node DeleteNode(Node node, string value)
        {
            Node temp = null;

            if (node != null)
            {
                if (String.Compare(node.value1, value) == 0)
                {
                    if (node.lChild == null && node.rChild == null)
                    {
                        return null;
                    }
                    else
                    {
                        if (node.lChild == null)
                        {
                            temp = node.rChild;
                            return temp;
                        }

                        if (node.rChild == null)
                        {
                            temp = node.lChild;
                            return temp;
                        }

                        string maxValue = FindMax();
                        node.value1 = maxValue;
                        node.lChild = DeleteNode(node.lChild, maxValue);
                    }
                }
                else
                {
                    if (String.Compare(node.value1, value) > 0)
                    {
                        node.lChild = DeleteNode(node.lChild, value);
                    }
                    else
                    {
                        node.rChild = DeleteNode(node.rChild, value);
                    }
                }
            }
            return node;
        }
    }


    class MainClass
    {
        //Endereço para a pasta que irá ser criada em C:\Users\NOME DO UTILIZADOR\Documents\dataGESTUR
        public static string pathDataGesrUrg = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\dataGESTUR";

        //Árvore para inserir os utentes
        public static Tree TreeUtentes = new Tree();

        //Array com os utentes inscritos (tamanho do array, número mínimo, numero máximo) - O array vai ser preenchido com números entre o mínimo e o máximo
        public static int[] utentesInscritos = ArrayAleatorio(100100, 0, 200000);

        //LISTAS
        public static List<Medico> listaMedicos = new List<Medico>();
        public static List<TecnicoAtendimento> listaTecnicoAtendimento = new List<TecnicoAtendimento>();
        public static List<Utente> listaUtentesNoCS = new List<Utente>();
        public static List<Utente> listaUtentes = new List<Utente>(); // lista de utentes registados no CS
        public static List<Urgencia> listaUrgencias = new List<Urgencia>();

        //FILAS ATENDIMENTO
        public static Queue filaAtendimento = new Queue();
        public static Queue filaRoxo = new Queue();
        public static Queue filaVermelho = new Queue();
        public static Queue filaAmarelo = new Queue();
        public static Queue filaVerde = new Queue();

        //VARIÁVEIS FILAS URGÊNCIA - usadas para a funcionalidade das filas de urgência de cada cor
        public static int inicio = 6;
        public static int indexR = 0;
        public static int indexVerm = 0;
        public static int indexA = 0;
        public static int indexVerde = 0;


        public static void Main(string[] args)
        {
            //TECNICOS DE ATENDIMENTO
            listaTecnicoAtendimento.Add(new TecnicoAtendimento(7, "Antero Sousa", "913740098", "anterosousa@hospital.eu", "Livre"));
            listaTecnicoAtendimento.Add(new TecnicoAtendimento(8, "Mónica Lima", "926931179", "monicalima@hospital.eu", "Livre"));
            listaTecnicoAtendimento.Add(new TecnicoAtendimento(9, "Inês Carreiró", "927456721", "inescarreiro@hospital.eu", "Livre"));

            // MÉDICOS
            listaMedicos.Add(new Medico(1, "Alberto Carvalho", "962481569", "albertocarvalho@hospital.eu", "Pediatria", "Livre", null));
            listaMedicos.Add(new Medico(2, "Joao Almeida", "932845692", "joaoalmeida@hospital.eu", "Psicologia", "Livre", null));
            listaMedicos.Add(new Medico(3, "Carina Sousa", "932845694", "carinasousa@hospital.eu", "Pediatria", "Livre", null));
            listaMedicos.Add(new Medico(4, "Beatriz Amado", "932845695", "beatrizamado@hospital.eu", "Cardiologia", "Livre", null));
            listaMedicos.Add(new Medico(5, "Casemiro Oliveira", "91745692", "casemirooliveira@hospital.eu", "Medicina Interna", "Livre", null));
            listaMedicos.Add(new Medico(6, "Susana Figueiredo", "912847403", "susanafigueiredo@hospital.eu", "Pneumologia", "Livre", null));

            // UTENTES PARA TESTAR - Com a ordem pela qual devem sair para consulta à frente
            //listaUtentesNoCS.Add(new Utente(45584, "João Silva", "969148135", "jsilva@gmail.com")); //0 -> 4º
            //listaUtentesNoCS.Add(new Utente(54645, "Alberto Carvalho", "968542189", "acarvalho@outlook.com")); //1 -> 8º
            //listaUtentesNoCS.Add(new Utente(44135, "Guilherme Arabolaza", "915823654", "guiara@icloud.com")); //2 ->  5º
            //listaUtentesNoCS.Add(new Utente(23154, "Rodrigo Almada", "912003587", "rodalmada@intern.pt")); //3 -> 1º
            //listaUtentesNoCS.Add(new Utente(71495, "Júlia Lopes", "912035987", "julia@email.com")); //4 -> 2º
            //listaUtentesNoCS.Add(new Utente(9531754, "Joana Bettencourt", "912054789", "bettencourtj@riot.com")); //5 -> 3º
            //listaUtentesNoCS.Add(new Utente(917544, "Maria Sotto-Mayor", "936857481", "sottomariamayor@gmail.com")); //6 -> 7º
            //listaUtentesNoCS.Add(new Utente(917154, "Joana Guimarães", "968529548", "joanaguimas@sapo.pt")); //7  ->10º
            //listaUtentesNoCS.Add(new Utente(912754, "Mariana Bastos", "968574157", "bastosmariana@hotmail.com")); //8  ->9º
            //listaUtentesNoCS.Add(new Utente(941754, "José Correia", "935698412", "josephco@gmail.com")); //9  -> 6º 

            //listaUtentes.Add(new Utente(45584, "João Silva", "969148135", "jsilva@gmail.com"));
            //listaUtentes.Add(new Utente(54645, "Alberto Carvalho", "968542189", "acarvalho@outlook.com"));
            //listaUtentes.Add(new Utente(44135, "Guilherme Arabolaza", "915823654", "guiara@icloud.com"));
            //listaUtentes.Add(new Utente(23154, "Rodrigo Almada", "912003587", "rodalmada@intern.pt"));
            //listaUtentes.Add(new Utente(71495, "Júlia Lopes", "912035987", "julia@email.com"));
            //listaUtentes.Add(new Utente(9531754, "Joana Bettencourt", "912054789", "bettencourtj@riot.com"));
            //listaUtentes.Add(new Utente(917544, "Maria Sotto-Mayor", "936857481", "sottomariamayor@gmail.com"));
            //listaUtentes.Add(new Utente(917154, "Joana Guimarães", "968529548", "joanaguimas@sapo.pt"));
            //listaUtentes.Add(new Utente(912754, "Mariana Bastos", "968574157", "bastosmariana@hotmail.com"));
            //listaUtentes.Add(new Utente(941754, "José Correia", "935698412", "josephco@gmail.com"));

            //Colocar os utentes para testar em diferentes Queues
            //filaVermelho.Enqueue(listaUtentesNoCS[0].NumUtente);
            //filaVerde.Enqueue(listaUtentesNoCS[1].NumUtente);
            //filaAmarelo.Enqueue(listaUtentesNoCS[2].NumUtente);
            //filaRoxo.Enqueue(listaUtentesNoCS[3].NumUtente);
            //filaRoxo.Enqueue(listaUtentesNoCS[4].NumUtente);
            //filaRoxo.Enqueue(listaUtentesNoCS[5].NumUtente);
            //filaAmarelo.Enqueue(listaUtentesNoCS[6].NumUtente);
            //filaVerde.Enqueue(listaUtentesNoCS[7].NumUtente);
            //filaAmarelo.Enqueue(listaUtentesNoCS[8].NumUtente);
            //filaVermelho.Enqueue(listaUtentesNoCS[9].NumUtente);

            //SENHAS FILA ATENDIMENTO - 20 senhas
            for (int i = 1; i < 21; i++)
            {
                filaAtendimento.Enqueue(i);
            }

            //Primeiro menu do programa
            bool displayMenu = true;
            while (displayMenu)
            {
                Console.Clear();
                displayMenu = PrimeiroMenu();
            }

        }


        //ARRAY ALEATÓRIO - função que criou o array com os números registados aleatórios
        public static int[] ArrayAleatorio(int tamanho, int min, int max)
        {

            int[] rArray = new int[tamanho];
            Random rnd = new Random();
            int aleatorio;

            for (int i = 0; i < 100000; i++) //Como o array em cima foi criado com 100100, só escrevi até 100000 para dar para registar Utentes novos no CS
            {

                aleatorio = rnd.Next(min, max);

                for (int j = i; j >= 0; j--)
                {

                    if (rArray[j] == aleatorio)
                    { aleatorio = rnd.Next(min, max); j = i; }

                }

                rArray[i] = aleatorio;

            }
            return rArray;
        }

        // MENU
        private static bool PrimeiroMenu()
        {

            //Escrita em ficheiros de texto 
            Directory.CreateDirectory(pathDataGesrUrg);
            using (StreamWriter sw = File.CreateText(pathDataGesrUrg + @"\dataGESTUR.txt"))
            {
                sw.WriteLine("GESTUR – Gestão de Urgências em Centro de Saúde");
                sw.WriteLine("");
                sw.WriteLine("1. Lista dos Médicos no Centro de Saúde");
                for (int i = 0; i < listaMedicos.Count; i++)
                {
                    sw.WriteLine("   " + (i + 1) + ". " + listaMedicos[i].Nome + ", " + listaMedicos[i].Especialidade + ", " + listaMedicos[i].Telefone);
                }


                sw.WriteLine("");
                sw.WriteLine("2. Filas de Espera dos Utentes nas Urgências");
                if (filaRoxo.Count > 0)
                {
                    sw.WriteLine("  Roxo (urgência imediata)");

                    int counter = 1;
                    foreach (int item in filaRoxo)
                    {
                        for (int i = 0; i < listaUtentesNoCS.Count; i++)
                        {
                            if (item == listaUtentesNoCS[i].NumUtente)
                            {
                                sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                counter++;
                            }
                        }
                    }
                }


                if (filaVermelho.Count > 0)
                {
                    sw.WriteLine("  Vermelho (muito urgente)");

                    int counter = 1;
                    foreach (int item in filaVermelho)
                    {
                        for (int i = 0; i < listaUtentesNoCS.Count; i++)
                        {
                            if (item == listaUtentesNoCS[i].NumUtente)
                            {
                                sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                counter++;
                            }
                        }
                    }
                }

                if (filaAmarelo.Count > 0)
                {
                    sw.WriteLine("  Amarelo (urgente)");

                    int counter = 1;
                    foreach (int item in filaAmarelo)
                    {
                        for (int i = 0; i < listaUtentesNoCS.Count; i++)
                        {
                            if (item == listaUtentesNoCS[i].NumUtente)
                            {
                                sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                counter++;
                            }
                        }
                    }
                }

                if (filaVerde.Count > 0)
                {
                    sw.WriteLine("  Verde (pouco urgente)");

                    int counter = 1;
                    foreach (int item in filaVerde)
                    {
                        for (int i = 0; i < listaUtentesNoCS.Count; i++)
                        {
                            if (item == listaUtentesNoCS[i].NumUtente)
                            {
                                sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                counter++;
                            }
                        }
                    }
                }


                sw.WriteLine("");

                if (listaUrgencias.Count > 0)
                {
                    sw.WriteLine("3. Lista das Urgências realizadas");

                    int counter = 1;
                    for (int i = 0; i < listaUrgencias.Count; i++)
                    {
                        sw.WriteLine("   " + counter + ". " + "Urgência: " + listaUrgencias[i].NumUrg + " | " + listaUrgencias[i].DataHora
                            + " | " + "Nº Utente: " + listaUrgencias[i].NumUtenteSaude);
                        sw.WriteLine("Médico: " + listaMedicos[listaUrgencias[i].NumFuncionarioMedico - 1].Nome);
                        sw.WriteLine(listaUrgencias[i].Relatorio);
                        sw.WriteLine("");
                        counter++;
                    }
                }



            }

            //"Imagem" do meno principal
            RedCross();

            //DVerm() - Cores // Resest() - voltar à cor normal do terminal
            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("    Pretende aceder à aplicação como:     ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("    1. Técnico de Atendimento             ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("    2. Médico                             ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                                          ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("    3. Adicionar Funcionário              ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("    4. Senha para Triagem                 ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("    5. Árvore Utentes (BST)               ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                                          ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("    6. Sair                               ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                                          ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.WriteLine("############################################");
            Reset();

            Console.WriteLine();
            Console.Write(" Opção pretendida: ");
            string result = Console.ReadLine();

            if (result == "1")
            {
                TecnicoAtenMenu();
                return true;
            }
            else if (result == "2")
            {
                MedicoMenu();
                return true;
            }
            else if (result == "3")
            {
                InscricaoNovosEmpregados();
                return true;
            }
            else if (result == "4")
            {
                filaAtendimento.Enqueue(filaAtendimento.Count + 1); //adiciona mais um, ao número de pessoas já existente na fila de atendimento
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(" Senha tirada com sucesso! Aguarde pela chamada feita pelo Técnico de Atendimento para realizar a triagem.");
                Console.WriteLine("______________________________________________________________" +
                    "____________________________________________\n Prima qualquer tecla para voltar ao menu.");

                Console.ReadKey();
                return true;
            }
            else if (result == "5")
            {
                Arvores();
                return true;
            }
            else if (result == "6")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //SUBMENU
        public static void MedicoMenu()
        {
            int indexmedico = 0; //determinar o index na lista de médicos, do médico que fez login
            bool repeatlogin = true;
            while (repeatlogin)
            {
                Console.Clear();
                Console.WriteLine();
                Console.Write(" Qual é o seu nº de médico? ");
                int nrmedico = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < listaMedicos.Count; i++) //verificar o index
                {
                    if (nrmedico == listaMedicos[i].NumFuncionario)
                    {
                        indexmedico = i;
                        repeatlogin = false;
                    }
                }
            }

            bool displayMenu = true;
            while (displayMenu)
            {
                loopingmenu:
                Console.Clear();
                Console.Write("Sessão iniciada por: " + listaMedicos[indexmedico].Nome + "                                           " +
                    "                        ");
                Console.Write("[");
                if (listaMedicos[indexmedico].Estado == "Livre") // cor consoante o estado
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(listaMedicos[indexmedico].Estado);
                Reset();
                Console.Write("]");
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine(" 1. Iniciar Urgência (Chamar Utente)\n" +
                    " 2. Finalizar Urgência (Escrever Relatório)\n" +
                    " 3. Urgências/Relatórios\n\n" +
                    " 4. Utentes no Centro de Saúde\n" +
                    " 5. Afixar Médicos/Fila de Espera/Urgências\n" +
                    " 6. Funcionários do Centro de Saúde\n\n" +
                    " 7. Mudar Utilizador\n" +
                    " 8. Sair");
                Console.WriteLine();
                Console.Write(" Opção pretendida: ");
                int escolha = Convert.ToInt32(Console.ReadLine());
                if (escolha < 1 | escolha > 8)
                    goto loopingmenu;

                switch (escolha)
                {
                    case 1: // INICIAR URGÊNCIA

                        if (listaMedicos[indexmedico].EmAtendimento == null)
                        {
                            if (filaRoxo.Count == 0 && filaVermelho.Count == 0 && filaAmarelo.Count == 0 && filaVerde.Count == 0)
                            {
                                Console.Clear();
                                Console.WriteLine();
                                Console.WriteLine(" Não existem utentes para serem atendidos. Prima qualquer tecla para voltar ao menu.");
                                Console.ReadKey();
                            }
                            else
                            {

                                Console.Clear();
                                Console.WriteLine();
                                Console.WriteLine();

                                if (filaRoxo.Count > 0)
                                {

                                    int primeiroElementoDequeue = Convert.ToInt32(filaRoxo.Peek()); //vê o nº de utente do primeiro elemento sair da filaRoxo
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (primeiroElementoDequeue == listaUtentesNoCS[i].NumUtente) //compara esse nº de utente com a lista de utentes no CS para verificar o index
                                        {
                                            indexR = i;
                                        }
                                    }

                                    filaRoxo.Dequeue(); // Remove da fila de Roxo
                                    listaMedicos[indexmedico].EmAtendimento = listaUtentesNoCS[indexR]; //insere o utente no médico que o está a atender 
                                    listaMedicos[indexmedico].Estado = "Em consulta"; //muda o estado do médico para em consulta


                                }
                                else //só acontece se não existir ninguém na fila roxo
                                {
                                    inicioswitch:
                                    inicio--; //vai entrar no switch e está por default a 6, diminui 1 sempre que passa aqui
                                    //vai entrar no switch como 5, e verificar se no caso 5 (vermelho) há alguém, se não existir
                                    //volta aqui, diminui um e o inicio fica agora 4 (amarelo) e repete-se até ao último caso 0.
                                    //no caso 0 (verde) o inicio volta a ser novamente 6 para o processo repetir-se

                                    switch (inicio)
                                    {

                                        case 5:
                                        case 3:
                                        case 1:
                                            if (filaVermelho.Count == 0)
                                            {
                                                goto inicioswitch;
                                            }
                                            int redpeek = Convert.ToInt32(filaVermelho.Peek());
                                            for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                            {
                                                if (redpeek == listaUtentesNoCS[i].NumUtente)
                                                {
                                                    indexVerm = i;
                                                }
                                            }
                                            filaVermelho.Dequeue();
                                            listaMedicos[indexmedico].EmAtendimento = listaUtentesNoCS[indexVerm];
                                            listaMedicos[indexmedico].Estado = "Em consulta";

                                            break;
                                        case 4:
                                        case 2:
                                            if (filaAmarelo.Count == 0)
                                            {
                                                goto inicioswitch;
                                            }
                                            int primeiroElementoDequeueb = Convert.ToInt32(filaAmarelo.Peek());

                                            for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                            {
                                                if (primeiroElementoDequeueb == listaUtentesNoCS[i].NumUtente)
                                                {
                                                    indexA = i;
                                                }
                                            }
                                            filaAmarelo.Dequeue();
                                            listaMedicos[indexmedico].EmAtendimento = listaUtentesNoCS[indexA];
                                            listaMedicos[indexmedico].Estado = "Em consulta";


                                            break;
                                        case 0:
                                            inicio = 6;
                                            if (filaVerde.Count == 0)
                                            {
                                                goto inicioswitch;
                                            }

                                            string a = Convert.ToString(filaVerde.Peek());
                                            for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                            {
                                                if (a == listaUtentesNoCS[i].Nome)
                                                {
                                                    indexVerde = i;
                                                }
                                            }
                                            filaVerde.Dequeue();
                                            listaMedicos[indexmedico].EmAtendimento = listaUtentesNoCS[indexVerde];
                                            listaMedicos[indexmedico].Estado = "Em consulta";


                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("Já tem um paciente em consulta, finalize primeiro o relatório da mesma.");
                            Console.WriteLine("__________________________________________________________________________________");
                            Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        break;

                    case 2: // FINALIZAR CONSULTA
                        Console.Clear();
                        if (listaMedicos[indexmedico].EmAtendimento != null)
                        {
                            int indexRemover = 0;
                            Console.Write("Sessão iniciada por: " + listaMedicos[indexmedico].Nome + "                                           " +
                    "                        ");
                            Console.Write("[");
                            if (listaMedicos[indexmedico].Estado == "Livre")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            Console.Write(listaMedicos[indexmedico].Estado);
                            Reset();
                            Console.Write("]");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine(" Utente número: " + listaMedicos[indexmedico].EmAtendimento.NumUtente);
                            Console.WriteLine(" Nome do paciente: " + listaMedicos[indexmedico].EmAtendimento.Nome);
                            Console.WriteLine(" Contactos do paciente: "
                                + listaMedicos[indexmedico].EmAtendimento.Telefone
                                + " - " + listaMedicos[indexmedico].EmAtendimento.Email);
                            Console.WriteLine();
                            Console.Write(" Relatório da Urgência: ");
                            string relatorio = Console.ReadLine();
                            listaUrgencias.Add(new Urgencia(Urgencia.UrgenciaCounter,
                                DateTime.UtcNow,
                                relatorio,
                                listaMedicos[indexmedico].NumFuncionario,
                                listaMedicos[indexmedico].EmAtendimento.NumUtente));

                            for (int i = 0; i < listaUtentesNoCS.Count; i++) //verificar o utente que o médico tem e ver o index que este tem na listadeutentes
                            {
                                if (listaMedicos[indexmedico].EmAtendimento.Nome == listaUtentesNoCS[i].Nome)
                                {
                                    indexRemover = i;
                                }
                            }


                            listaUtentesNoCS.RemoveAt(indexRemover);
                            listaMedicos[indexmedico].EmAtendimento = null;
                            listaMedicos[indexmedico].Estado = "Livre";
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" Consulta terminada com sucesso. Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("Não existem utentes no gabinete. Inicie uma consulta para chamar um novo paciente.");
                            Console.WriteLine("__________________________________________________________________________________");
                            Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }

                        break;

                    case 3: // URGÊNCIAS RELATÓRIOS
                        Console.Clear();
                        if (listaUrgencias.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" Não existem consultas disponíveis. Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        else
                        {
                            for (int i = 0; i < listaUrgencias.Count; i++)
                            {
                                Console.WriteLine();
                                Console.WriteLine();
                                VermT();
                                Console.Write(" Médico: ");
                                Reset();
                                for (int j = 0; j < listaMedicos.Count; j++)
                                {
                                    if (listaUrgencias[i].NumFuncionarioMedico == listaMedicos[j].NumFuncionario)
                                    {
                                        Console.Write(listaMedicos[j].Nome);
                                    }
                                }

                                Console.WriteLine(" (nº " + listaUrgencias[i].NumFuncionarioMedico + ")");

                                for (int j = 0; j < listaUtentes.Count; j++)
                                {
                                    if (listaUrgencias[i].NumUtenteSaude == listaUtentes[j].NumUtente)
                                    {
                                        VermT();
                                        Console.Write(" Paciente: ");
                                        Reset();
                                        Console.Write(listaUtentes[j].Nome);
                                    }
                                }

                                Console.WriteLine(" (nº " + listaUrgencias[i].NumUtenteSaude + ")");
                                VermT();
                                Console.Write(" Relatório Médico: ");
                                Reset();
                                Console.WriteLine(listaUrgencias[i].Relatorio);
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        break;

                    case 4: // UTENTES NO CS
                        Console.Clear();
                        if (listaUtentesNoCS.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" Não existem utentes no Centro de Saúde. Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("__________________________________________________________________________________");
                            Console.WriteLine();
                            Console.WriteLine("  Lista de Utentes no Centro de Saúde");
                            Console.WriteLine("__________________________________________________________________________________");
                            for (int i = 0; i < listaUtentesNoCS.Count; i++)
                            {
                                Console.WriteLine();
                                Console.WriteLine();
                                VermT();
                                Console.Write(" Nº de Utente: ");
                                Reset();
                                Console.WriteLine(listaUtentesNoCS[i].NumUtente);
                                VermT();
                                Console.Write(" Nome Completo: ");
                                Reset();
                                Console.WriteLine(listaUtentesNoCS[i].Nome);
                                VermT();
                                Console.Write(" Contactos do utente: ");
                                Reset();
                                Console.WriteLine(listaUtentesNoCS[i].Telefone + " - " + listaUtentesNoCS[i].Email);
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }

                        break;

                    case 5: // AFIXAR FICHEIRO TEXTO
                        using (StreamWriter sw = File.CreateText(pathDataGesrUrg + @"\dataGESTUR.txt"))
                        {
                            sw.WriteLine("GESTUR – Gestão de Urgências em Centro de Saúde");
                            sw.WriteLine("");
                            sw.WriteLine("1. Lista dos Médicos no Centro de Saúde");
                            for (int i = 0; i < listaMedicos.Count; i++)
                            {
                                sw.WriteLine("   " + (i + 1) + ". " + listaMedicos[i].Nome + ", " + listaMedicos[i].Especialidade + ", " + listaMedicos[i].Telefone);
                            }


                            sw.WriteLine("");
                            sw.WriteLine("2. Filas de Espera dos Utentes nas Urgências");
                            if (filaRoxo.Count > 0)
                            {
                                sw.WriteLine("  Roxo (urgência imediata)");

                                int counter = 1;
                                foreach (int item in filaRoxo)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }


                            if (filaVermelho.Count > 0)
                            {
                                sw.WriteLine("  Vermelho (muito urgente)");

                                int counter = 1;
                                foreach (int item in filaVermelho)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }

                            if (filaAmarelo.Count > 0)
                            {
                                sw.WriteLine("  Amarelo (urgente)");

                                int counter = 1;
                                foreach (int item in filaAmarelo)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }

                            if (filaVerde.Count > 0)
                            {
                                sw.WriteLine("  Verde (pouco urgente)");

                                int counter = 1;
                                foreach (int item in filaVerde)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }


                            sw.WriteLine("");

                            if (listaUrgencias.Count > 0)
                            {
                                sw.WriteLine("3. Lista das Urgências realizadas");

                                int counter = 1;
                                for (int i = 0; i < listaUrgencias.Count; i++)
                                {
                                    sw.WriteLine("   " + counter + ". " + "Urgência: " + listaUrgencias[i].NumUrg + " | " + listaUrgencias[i].DataHora
                                        + " | " + "Nº Utente: " + listaUrgencias[i].NumUtenteSaude);
                                    sw.WriteLine("Médico: " + listaMedicos[listaUrgencias[i].NumFuncionarioMedico - 1].Nome);
                                    sw.WriteLine(listaUrgencias[i].Relatorio);
                                    sw.WriteLine("");
                                    counter++;
                                }
                            }



                        }
                        Console.Clear();
                        string[] lines = File.ReadAllLines(pathDataGesrUrg + @"\dataGESTUR.txt");
                        foreach (string line in lines)
                        {
                            Console.WriteLine("\t" + line);
                        }
                        Console.WriteLine("__________________________________________________________________________________");
                        Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                        Console.ReadKey();
                        break;

                    case 6: // FUNCIONÁRIOS
                        Console.Clear();

                        bool lopio = true;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("   Deseja ver a lista dos:\n" +
                                " 1. Médicos\n" +
                                " 2. Técnicos de Atendimento ");
                            Console.WriteLine();
                            Console.Write(" Opção pretendida: ");
                            string medtecn = Console.ReadLine();

                            if (medtecn == "1")
                            {
                                Console.Clear();
                                Console.WriteLine("__________________________________________________________________________________");
                                Console.WriteLine();
                                Console.WriteLine("  Lista de Médicos do Centro de Saúde");
                                Console.WriteLine("__________________________________________________________________________________");

                                for (int i = 0; i < listaMedicos.Count; i++)
                                {
                                    Console.WriteLine();
                                    DCyan(); // cores
                                    Console.Write(" Nº do Médico: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].NumFuncionario);
                                    DCyan();
                                    Console.Write(" Nome Completo: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].Nome);
                                    DCyan();
                                    Console.Write(" Especialidade do Médico: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].Especialidade);
                                    DCyan();
                                    Console.Write(" Contactos do Médico: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].Telefone + " - " + listaMedicos[i].Email);
                                }
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                                Console.ReadKey();
                                break;
                            }
                            else if (medtecn == "2")
                            {
                                Console.Clear();
                                Console.WriteLine("__________________________________________________________________________________");
                                Console.WriteLine();
                                Console.WriteLine(" Lista de Técnicos de Atendimento do Centro de Saúde");
                                Console.WriteLine("__________________________________________________________________________________");

                                for (int i = 0; i < listaTecnicoAtendimento.Count; i++)
                                {
                                    Console.WriteLine();
                                    DCyan();
                                    Console.Write(" Nº do Técnico de Atendimento: ");
                                    Reset();
                                    Console.WriteLine(listaTecnicoAtendimento[i].NumFuncionario);
                                    DCyan();
                                    Console.Write(" Nome Completo: ");
                                    Reset();
                                    Console.WriteLine(listaTecnicoAtendimento[i].Nome);
                                    DCyan();
                                    Console.Write(" Contactos do Técnico de Atendimento: ");
                                    Reset();
                                    Console.WriteLine(listaTecnicoAtendimento[i].Telefone + " - " + listaTecnicoAtendimento[i].Email);
                                }
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                lopio = true;
                            }
                        } while (lopio);

                        break;
                    case 7: // MUDAR UTILIZADOR
                        displayMenu = false;
                        Console.Clear();
                        break;

                    case 8: // SAIR
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void TecnicoAtenMenu()
        {
            int indexfuncionario = 0; //determinar o index na lista de técnicos, do técnico que fez login
            bool repeatlogin = true;
            while (repeatlogin)
            {
                Console.Clear();
                Console.WriteLine();
                Console.Write(" Qual é o seu nº de técnico de atendimento? ");
                int nrtecaten = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < listaTecnicoAtendimento.Count; i++)
                {
                    if (nrtecaten == listaTecnicoAtendimento[i].NumFuncionario)
                    {
                        indexfuncionario = i;
                        repeatlogin = false;
                    }
                }
            }

            bool displayMenu = true;
            while (displayMenu)
            {
                loopingmenu:
                Console.Clear();
                Console.Write("Sessão iniciada por: " + listaTecnicoAtendimento[indexfuncionario].Nome + "                           " +
                    "                                     ");
                Console.Write("[");
                if (listaTecnicoAtendimento[indexfuncionario].Estado == "Livre")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(listaTecnicoAtendimento[indexfuncionario].Estado);
                Reset();
                Console.Write("]");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("            Pessoas por atender: " + filaAtendimento.Count);
                Console.WriteLine();
                Console.WriteLine(" 1. Atender Utente\n" +
                    " 2. Utentes no Centro de Saúde\n\n" +
                    " 3. Urgências/Relatórios\n" +
                    " 4. Afixar Médicos/Fila de Espera/Urgências\n" +
                    " 5. Funcionários do Centro de Saúde\n\n" +
                    " 6. Mudar Utilizador\n" +
                    " 7. Sair");
                Console.WriteLine();
                Console.Write(" Opção pretendida: ");

                int escolha = Convert.ToInt32(Console.ReadLine());
                if (escolha < 1 | escolha > 7)
                    goto loopingmenu;

                switch (escolha)
                {
                    case 1: // ATENDER UTENTE
                        listaTecnicoAtendimento[indexfuncionario].Estado = "Em atendimento";
                        Console.Clear();
                        Console.Write("Sessão iniciada por: " + listaTecnicoAtendimento[indexfuncionario].Nome + "                           " +
                            "                                     ");
                        Console.Write("[");
                        if (listaTecnicoAtendimento[indexfuncionario].Estado == "Livre")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write(listaTecnicoAtendimento[indexfuncionario].Estado);
                        Reset();
                        Console.Write("]");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        filaAtendimento.Dequeue();
                        Console.WriteLine("      INFORMAÇÕES DO UTENTE      ");
                        Console.Write(" Nome completo do utente: ");
                        string nomeUtenteATEN = Console.ReadLine();
                        Console.Write(" Número de saúde do utente: ");
                        int numUtenteATEN = Convert.ToInt32(Console.ReadLine());
                        Console.Write(" Número de telemóvel do utente: ");
                        string telefoneATEN = Console.ReadLine();
                        Console.Write(" Email do utente: ");
                        string emailATEN = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine(" Aguarde um momento. A verificar se o número está inscrito no Centro de saúde.");

                        SelectSort(utentesInscritos); // usar selection sort para organizar o array

                        if (VerificarNumUtente(numUtenteATEN)) //linear search com o array já organizado para ver se o nº está no array
                        {
                            Console.Clear();
                            Console.Write("Sessão iniciada por: " + listaTecnicoAtendimento[indexfuncionario].Nome + "                           " +
     "                                     ");
                            Console.Write("[");
                            if (listaTecnicoAtendimento[indexfuncionario].Estado == "Livre")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            Console.Write(listaTecnicoAtendimento[indexfuncionario].Estado);
                            Reset();
                            Console.Write("]");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("O utente já se encontra registado no Centro de Saúde!\n");
                            Console.WriteLine("___________________________________" +
                                "_______________________________________________________________________");
                            Console.WriteLine(" Prima qualquer tecla para prosseguir para a triagem.");
                            listaUtentesNoCS.Add(new Utente(numUtenteATEN, nomeUtenteATEN, telefoneATEN, emailATEN));
                            listaUtentes.Add(new Utente(numUtenteATEN, nomeUtenteATEN, telefoneATEN, emailATEN));
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write("Sessão iniciada por: " + listaTecnicoAtendimento[indexfuncionario].Nome + "                           " +
                                "                                     ");
                            Console.Write("[");
                            if (listaTecnicoAtendimento[indexfuncionario].Estado == "Livre")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            Console.Write(listaTecnicoAtendimento[indexfuncionario].Estado);
                            Reset();
                            Console.Write("]");
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine(" O utente não se encontrava registado no Centro de Saúde! " +
                                "O seu registo foi efectuado com sucesso.\n");
                            Console.WriteLine("___________________________________" +
                                "_______________________________________________________________________");
                            Console.WriteLine(" Prima qualquer tecla para prosseguir para a triagem.");
                            InscricaoCSaude(numUtenteATEN); // inscrição do utente do array
                            listaUtentesNoCS.Add(new Utente(numUtenteATEN, nomeUtenteATEN, telefoneATEN, emailATEN));
                            listaUtentes.Add(new Utente(numUtenteATEN, nomeUtenteATEN, telefoneATEN, emailATEN));
                        }
                        Console.ReadKey();
                        bool repetir = true;
                        do
                        {
                            Console.Clear();
                            Console.Write("Sessão iniciada por: " + listaTecnicoAtendimento[indexfuncionario].Nome + "                           " +
                                "                                     ");
                            Console.Write("[");
                            if (listaTecnicoAtendimento[indexfuncionario].Estado == "Livre")
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            Console.Write(listaTecnicoAtendimento[indexfuncionario].Estado);
                            Reset();
                            Console.Write("]");
                            Console.WriteLine();
                            Console.WriteLine();

                            Console.WriteLine("______________________________________________" +
                                "____________________________________________________________");
                            Console.WriteLine();
                            Console.WriteLine("                                                  TRIAGEM");
                            Console.WriteLine("______________________________________________" +
                                "____________________________________________________________");
                            Console.WriteLine();
                            Console.WriteLine(" Escreva a cor desejada para a correspondente triagem efectuada");
                            Console.Write(" // Urgência imediata (");
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write("ROXO");
                            Reset();
                            Console.Write(") // ");
                            Console.Write("Muito Urgente (");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("VERMELHO");
                            Reset();
                            Console.Write(") // ");
                            Console.Write("Urgente (");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("AMARELO");
                            Reset();
                            Console.Write(") // ");
                            Console.Write("Pouco Urgente (");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("VERDE");
                            Reset();
                            Console.Write(") //\n");
                            Console.WriteLine();
                            Console.Write(" Cor de urgência designada para o utente " + nomeUtenteATEN + ": ");
                            string triagem = Console.ReadLine();

                            if (triagem.ToLower() == "roxo")
                            {
                                for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                {
                                    if (nomeUtenteATEN == listaUtentesNoCS[i].Nome)
                                    {
                                        filaRoxo.Enqueue(listaUtentesNoCS[i].NumUtente);
                                    }
                                }
                                break;
                            }
                            else if (triagem.ToLower() == "vermelho")
                            {
                                for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                {
                                    if (nomeUtenteATEN == listaUtentesNoCS[i].Nome)
                                    {
                                        filaVermelho.Enqueue(listaUtentesNoCS[i].NumUtente);
                                    }
                                }
                                break;
                            }
                            else if (triagem.ToLower() == "amarelo")
                            {
                                for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                {
                                    if (nomeUtenteATEN == listaUtentesNoCS[i].Nome)
                                    {
                                        filaAmarelo.Enqueue(listaUtentesNoCS[i].NumUtente);
                                    }
                                }
                                break;
                            }
                            else if (triagem.ToLower() == "verde")
                            {
                                for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                {
                                    if (nomeUtenteATEN == listaUtentesNoCS[i].Nome)
                                    {
                                        filaVerde.Enqueue(listaUtentesNoCS[i].NumUtente);
                                    }
                                }
                                break;
                            }
                            else
                            {
                                repetir = true;
                            }
                        } while (repetir);
                        Console.WriteLine();
                        listaTecnicoAtendimento[indexfuncionario].Estado = "Livre";
                        Console.WriteLine("__________________________________________________________________________________________________________");
                        Console.WriteLine("O utente foi registado com sucesso! Prima qualquer tecla para voltar ao menu.");
                        Console.ReadKey();
                        break;

                    case 2: // Utentes no Centro de Saúde
                        Console.Clear();
                        if (listaUtentesNoCS.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" Não existem utentes no Centro de Saúde. Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("__________________________________________________________________________________");
                            Console.WriteLine();
                            Console.WriteLine("  Lista de Utentes no Centro de Saúde");
                            Console.WriteLine("__________________________________________________________________________________");
                            for (int i = 0; i < listaUtentesNoCS.Count; i++)
                            {
                                Console.WriteLine();
                                Console.WriteLine();
                                VermT();
                                Console.Write(" Nº de Utente: ");
                                Reset();
                                Console.WriteLine(listaUtentesNoCS[i].NumUtente);
                                VermT();
                                Console.Write(" Nome Completo: ");
                                Reset();
                                Console.WriteLine(listaUtentesNoCS[i].Nome);
                                VermT();
                                Console.Write(" Contactos do utente: ");
                                Reset();
                                Console.WriteLine(listaUtentesNoCS[i].Telefone + " - " + listaUtentesNoCS[i].Email);
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        break;


                    case 3: // URGÊNCIAS RELATÓRIOS
                        Console.Clear();
                        if (listaUrgencias.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" Não existem consultas disponíveis. Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        else
                        {
                            for (int i = 0; i < listaUrgencias.Count; i++)
                            {
                                Console.WriteLine();
                                Console.WriteLine();
                                VermT();
                                Console.Write(" Médico: ");
                                Reset();
                                for (int j = 0; j < listaMedicos.Count; j++)
                                {
                                    if (listaUrgencias[i].NumFuncionarioMedico == listaMedicos[j].NumFuncionario)
                                    {
                                        Console.Write(listaMedicos[j].Nome);
                                    }
                                }

                                Console.WriteLine(" (nº " + listaUrgencias[i].NumFuncionarioMedico + ")");

                                for (int j = 0; j < listaUtentes.Count; j++)
                                {
                                    if (listaUrgencias[i].NumUtenteSaude == listaUtentes[j].NumUtente)
                                    {
                                        VermT();
                                        Console.Write(" Paciente: ");
                                        Reset();
                                        Console.Write(listaUtentes[j].Nome);
                                    }
                                }

                                Console.WriteLine(" (nº " + listaUrgencias[i].NumUtenteSaude + ")");
                                VermT();
                                Console.Write(" Relatório Médico: ");
                                Reset();
                                Console.WriteLine(listaUrgencias[i].Relatorio);
                            }
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                            Console.ReadKey();
                        }
                        break;

                    case 4:

                        using (StreamWriter sw = File.CreateText(pathDataGesrUrg + @"\dataGESTUR.txt"))
                        {
                            sw.WriteLine("GESTUR – Gestão de Urgências em Centro de Saúde");
                            sw.WriteLine("");
                            sw.WriteLine("1. Lista dos Médicos no Centro de Saúde");
                            for (int i = 0; i < listaMedicos.Count; i++)
                            {
                                sw.WriteLine("   " + (i + 1) + ". " + listaMedicos[i].Nome + ", " + listaMedicos[i].Especialidade + ", " + listaMedicos[i].Telefone);
                            }


                            sw.WriteLine("");
                            sw.WriteLine("2. Filas de Espera dos Utentes nas Urgências");
                            if (filaRoxo.Count > 0)
                            {
                                sw.WriteLine("  Roxo (urgência imediata)");

                                int counter = 1;
                                foreach (int item in filaRoxo)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }


                            if (filaVermelho.Count > 0)
                            {
                                sw.WriteLine("  Vermelho (muito urgente)");

                                int counter = 1;
                                foreach (int item in filaVermelho)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }

                            if (filaAmarelo.Count > 0)
                            {
                                sw.WriteLine("  Amarelo (urgente)");

                                int counter = 1;
                                foreach (int item in filaAmarelo)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }

                            if (filaVerde.Count > 0)
                            {
                                sw.WriteLine("  Verde (pouco urgente)");

                                int counter = 1;
                                foreach (int item in filaVerde)
                                {
                                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                                    {
                                        if (item == listaUtentesNoCS[i].NumUtente)
                                        {
                                            sw.WriteLine("   " + counter + ". " + listaUtentesNoCS[i].Nome);
                                            counter++;
                                        }
                                    }
                                }
                            }


                            sw.WriteLine("");

                            if (listaUrgencias.Count > 0)
                            {
                                sw.WriteLine("3. Lista das Urgências realizadas");

                                int counter = 1;
                                for (int i = 0; i < listaUrgencias.Count; i++)
                                {
                                    sw.WriteLine("   " + counter + ". " + "Urgência: " + listaUrgencias[i].NumUrg + " | " + listaUrgencias[i].DataHora
                                        + " | " + "Nº Utente: " + listaUrgencias[i].NumUtenteSaude);
                                    sw.WriteLine("Médico: " + listaMedicos[listaUrgencias[i].NumFuncionarioMedico - 1].Nome);
                                    sw.WriteLine(listaUrgencias[i].Relatorio);
                                    sw.WriteLine("");
                                    counter++;
                                }
                            }



                        }
                        Console.Clear();
                        string[] lines = File.ReadAllLines(pathDataGesrUrg + @"\dataGESTUR.txt");
                        foreach (string line in lines)
                        {
                            Console.WriteLine("\t" + line);
                        }
                        Console.WriteLine("__________________________________________________________________________________");
                        Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                        Console.ReadKey();
                        break;


                    case 5: //FUNCIONÁRIOS C.S
                        Console.Clear();

                        bool lopio = true;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("   Deseja ver a lista dos:\n" +
                                " 1. Médicos\n" +
                                " 2. Técnicos de Atendimento ");
                            Console.WriteLine();
                            Console.Write(" Opção pretendida: ");
                            string medtecn = Console.ReadLine();

                            if (medtecn == "1")
                            {
                                Console.Clear();
                                Console.WriteLine("__________________________________________________________________________________");
                                Console.WriteLine();
                                Console.WriteLine("  Lista de Médicos do Centro de Saúde");
                                Console.WriteLine("__________________________________________________________________________________");

                                for (int i = 0; i < listaMedicos.Count; i++)
                                {
                                    Console.WriteLine();
                                    DCyan();
                                    Console.Write(" Nº do Médico: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].NumFuncionario);
                                    DCyan();
                                    Console.Write(" Nome Completo: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].Nome);
                                    DCyan();
                                    Console.Write(" Especialidade do Médico: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].Especialidade);
                                    DCyan();
                                    Console.Write(" Contactos do Médico: ");
                                    Reset();
                                    Console.WriteLine(listaMedicos[i].Telefone + " - " + listaMedicos[i].Email);
                                }
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                                Console.ReadKey();
                                break;
                            }
                            else if (medtecn == "2")
                            {
                                Console.Clear();
                                Console.WriteLine("__________________________________________________________________________________");
                                Console.WriteLine();
                                Console.WriteLine(" Lista de Técnicos de Atendimento do Centro de Saúde");
                                Console.WriteLine("__________________________________________________________________________________");

                                for (int i = 0; i < listaTecnicoAtendimento.Count; i++)
                                {
                                    Console.WriteLine();
                                    DCyan();
                                    Console.Write(" Nº do Técnico de Atendimento: ");
                                    Reset();
                                    Console.WriteLine(listaTecnicoAtendimento[i].NumFuncionario);
                                    DCyan();
                                    Console.Write(" Nome Completo: ");
                                    Reset();
                                    Console.WriteLine(listaTecnicoAtendimento[i].Nome);
                                    DCyan();
                                    Console.Write(" Contactos do Técnico de Atendimento: ");
                                    Reset();
                                    Console.WriteLine(listaTecnicoAtendimento[i].Telefone + " - " + listaTecnicoAtendimento[i].Email);
                                }
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                lopio = true;
                            }
                        } while (lopio);

                        break;
                    case 6:
                        displayMenu = false;
                        Console.Clear();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void InscricaoNovosEmpregados()
        {

            int novoNumero;
            string novoNome, novoTelefone, novoEmail, novoEspecialidade;



            bool lopio = true;
            do
            {
                Console.Clear();
                Console.WriteLine("   Adicionar novo:\n" +
                    " 1. Médico\n" +
                    " 2. Técnico de Atendimento ");
                Console.WriteLine();
                Console.Write(" Opção pretendida: ");
                string medtecn = Console.ReadLine();

                if (medtecn == "1")
                {
                    numerojaexiste:
                    Console.Clear();
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.WriteLine();
                    Console.WriteLine("  Adicionar novo Médico ao Centro de Saúde");
                    Console.WriteLine("__________________________________________________________________________________");

                    Console.WriteLine();
                    Console.WriteLine(" Qual o nº do médico? ");
                    DCyan();
                    Console.Write(" Número: ");
                    Reset();
                    novoNumero = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaMedicos.Count; i++)
                    {
                        if (novoNumero == listaMedicos[i].NumFuncionario)
                        {
                            goto numerojaexiste;
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine(" Qual o nome do médico?");
                    DCyan();
                    Console.Write(" Nome: ");
                    Reset();
                    novoNome = Console.ReadLine();

                    Console.WriteLine();
                    Console.WriteLine(" Qual o nº de telefone do médico?");
                    DCyan();
                    Console.Write(" Telefone: ");
                    Reset();
                    novoTelefone = Console.ReadLine();

                    Console.WriteLine();
                    Console.WriteLine(" Qual o email do médico?");
                    DCyan();
                    Console.Write(" Email: ");
                    Reset();
                    novoEmail = Console.ReadLine();

                    Console.WriteLine();
                    Console.WriteLine(" Qual a especialidade do médico?");
                    DCyan();
                    Console.Write(" Especialidade: ");
                    Reset();
                    novoEspecialidade = Console.ReadLine();

                    listaMedicos.Add(new Medico(novoNumero, novoNome, novoTelefone, novoEmail, novoEspecialidade, "Livre", null));

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("O novo médico foi adicionado com sucesso ao Centro de Saúde!");
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    break;
                }
                else if (medtecn == "2")
                {
                    numerojaexiste:
                    Console.Clear();
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.WriteLine();
                    Console.WriteLine("  Adicionar novo Técnico de Atendimento ao Centro de Saúde");
                    Console.WriteLine("__________________________________________________________________________________");

                    Console.WriteLine();
                    Console.WriteLine(" Qual o nº de técnico de atendimento? ");
                    DCyan();
                    Console.Write(" Número: ");
                    Reset();
                    novoNumero = Convert.ToInt32(Console.ReadLine());
                    for (int i = 0; i < listaTecnicoAtendimento.Count; i++)
                    {
                        if (novoNumero == listaTecnicoAtendimento[i].NumFuncionario)
                        {
                            goto numerojaexiste;
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine(" Qual o nome do técnico?");
                    DCyan();
                    Console.Write(" Nome: ");
                    Reset();
                    novoNome = Console.ReadLine();

                    Console.WriteLine();
                    Console.WriteLine(" Qual o nº de telefone do técnico?");
                    DCyan();
                    Console.Write(" Telefone: ");
                    Reset();
                    novoTelefone = Console.ReadLine();

                    Console.WriteLine();
                    Console.WriteLine(" Qual o email do técnico?");
                    DCyan();
                    Console.Write(" Email: ");
                    Reset();
                    novoEmail = Console.ReadLine();


                    listaTecnicoAtendimento.Add(new TecnicoAtendimento(novoNumero, novoNome, novoTelefone, novoEmail, "Livre"));

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("O novo técnico de atendimento foi adicionado com sucesso ao Centro de Saúde!");
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    lopio = true;
                }
            } while (lopio);
        }
        public static void Arvores()
        {


            bool lopio = true;
            do
            {
                repeat:
                Console.Clear();
                Console.WriteLine("Árvore Utentes (BST)");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine(" 1. Apresentar Utentes no ecrã\n" +
                    " 2. Pesquisar Utente\n" +
                    " 3. Eliminar Utente\n" +
                    " 4. Inserir Utentes na Árvore\n" +
                    " 5. Voltar ao menu");
                Console.WriteLine("__________________________________________________________________________________");
                Console.WriteLine();
                Console.Write(" Opção pretendida: ");
                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Lista dos Utentes (Ordem Alfabética)");
                    Console.WriteLine("__________________________________________________________________________________");
                    TreeUtentes.PrintInOrder();
                    Console.WriteLine();
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    goto repeat;
                }
                else if (opcao == "2")
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Pesquisa na Lista dos Utentes");
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.Write("Nome do utente que deseja pesquisar: ");
                    string nomeapesquisar = Console.ReadLine();
                    if (TreeUtentes.Find(nomeapesquisar))
                    {
                        Console.WriteLine();
                        Console.WriteLine("          O nome encontra-se na lista de utentes!");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("          O nome não se encontra na lista de utentes!");
                    }

                    Console.WriteLine();
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    goto repeat;
                }
                else if (opcao == "3")
                {

                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("Eliminar nome da Árvore");
                    Console.WriteLine("__________________________________________________________________________________");
                    Console.Write("Nome do utente que deseja eliminar: ");
                    string nomeaeliminar = Console.ReadLine();
                    if (TreeUtentes.Find(nomeaeliminar))
                    {
                        TreeUtentes.DeleteNode(nomeaeliminar);
                        if (TreeUtentes.Find(nomeaeliminar) == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("    O nome " + nomeaeliminar + " foi eliminado com sucesso da árvore BST");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Ocorreu um erro a realizar o pretendido!");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("    O nome que pretende eliminar não existe na árvore BST!");
                        Console.WriteLine();
                    }

                    Console.WriteLine("__________________________________________________________________________________");
                    Console.WriteLine("Prima qualquer tecla para voltar ao menu.");
                    Console.ReadKey();
                    goto repeat;
                }
                else if (opcao == "4")
                {

                    for (int j = 0; j < listaUtentesNoCS.Count; j++)
                    {
                        while (TreeUtentes.Find(listaUtentesNoCS[j].Nome))
                        {
                            for (int i = 0; i < listaUtentesNoCS.Count; i++)
                            {
                                TreeUtentes.DeleteNode(listaUtentesNoCS[i].Nome);
                            }
                        }
                    }

                    for (int i = 0; i < listaUtentesNoCS.Count; i++)
                    {
                        TreeUtentes.InsertNode(listaUtentesNoCS[i].Nome);
                    }

                    goto repeat;
                }
                else if (opcao == "5")
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("    Prima qualquer tecla.");
                    break;
                }

                else
                {
                    lopio = true;
                }
            } while (lopio);
        }

        // LINEAR SEARCH (Verificar nº Utente no Array) 
        public static bool VerificarNumUtente(int numeroPesquisarNoArray)
        {
            for (int i = 0; i < utentesInscritos.Length; i++)
            {
                if (numeroPesquisarNoArray == utentesInscritos[i])
                    return true;
                else if (numeroPesquisarNoArray < utentesInscritos[i])
                    return false;
            }
            return false;
        }

        //INSCRIÇÃO NO CS 
        public static void InscricaoCSaude(int _numeroUtente)
        {
            for (int i = 0; i < utentesInscritos.Length; i++)
            {
                if (utentesInscritos[i] == 0)
                {
                    utentesInscritos[i] = _numeroUtente;
                    break;
                }
            }
        }

        //SELECTION SORT
        private static void SelectSort(int[] arr)
        {
            int pos_min, temp;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                pos_min = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[pos_min])
                        pos_min = j;
                }
                if (pos_min != i)
                {
                    temp = arr[i];
                    arr[i] = arr[pos_min];
                    arr[pos_min] = temp;
                }
            }
        }

        //CORES
        public static void Verm()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void DVerm()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        public static void VermT()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void DCyan()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
        public static void Reset()
        {
            Console.ResetColor();
        }
        public static void RedCross()
        {

            DVerm();
            Console.WriteLine("############################################");
            Console.Write("#");
            Reset();
            Console.Write("                                          ");
            DVerm();
            Console.Write("#\n");
            Reset();
            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                    ");
            Console.Write("                      ");
            DVerm();
            Console.Write("#\n");

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("            Serviço de Urgências  ");
            Console.Write("        ");
            DVerm();
            Console.Write("#\n");

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                    ");
            Console.Write("                      ");
            DVerm();
            Console.Write("#\n");




            Console.Write("#");
            Reset();
            Console.Write("                    ");
            Verm();
            Console.Write("###");
            Reset();
            Console.Write("                   ");
            DVerm();
            Console.Write("#\n");
            Reset();


            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                    ");
            Verm();
            Console.Write("###");
            Reset();
            Console.Write("                   ");
            DVerm();
            Console.Write("#\n");
            Reset();

            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                 ");
            Verm();
            Console.Write("#########");
            Reset();
            Console.Write("                ");
            DVerm();
            Console.Write("#\n");
            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                    ");
            Verm();
            Console.Write("###");
            Reset();
            Console.Write("                   ");
            DVerm();
            Console.Write("#\n");
            Reset();
            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                    ");
            Verm();
            Console.Write("###");
            Reset();
            Console.Write("                   ");
            DVerm();
            Console.Write("#\n");
            Reset();
            DVerm();
            Console.Write("#");
            Reset();
            Console.Write("                    ");
            Console.Write("                      ");
            DVerm();
            Console.Write("#\n");
            Reset();
        }
    }


}