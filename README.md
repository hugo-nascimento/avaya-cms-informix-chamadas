# avaya-cms-informix-chamadas 📊📞⚙
Conexão com banco de dados IBM Informix do Avaya CMS - Tabela hsplit

Ideal para desenvolvedores, analistas de MIS ou BI que desejam conectar direto no banco de dados do Avaya CMS e consumir os dados disponíveis nas tabelas.

A vantagem é que desta forma, não é necessária nenhuma interação direta com o Avaya CMS para extração dos dados.

Este projeto, consulta os dados da tabela hsplit, que contém dados de chamadas como:

✅ Chamadas Recebidas <br>
✅ Chamadas Atendidas <br>
✅ Chamadas Abandonadas <br>
✅ Chamadas Realizadas <br>
✅ Chamadas Transferidas <br>
✅ Tempo Falado <br>
✅ Tempo HOLD <br>
✅ Tempo ACW (After Call Work) <br>
✅ Tempo Logado <br>
✅ Tempo em Pausa <br>

Esta tabela hsplit contém os dados disponíveis no relatório do AVAYA CMS MASTER REPORT

Dois pontos importantes que devem ser considerados para a utilização dos dados do Informix:

1️⃣ Seu usuário do CMS deve ter permissão para acesso ao Informix, que deve ser solicitada à área de Telecom da empresa, que verificará essa possibilidade.

2️⃣ É necessário ter instalado o IBM Informix Client-SDK na máquina onde irá rodar o processo. 

Após a liberação de acesso ao Informix e a instalação do SDK, é necessário configurar o DSN para acessar o Database.

![odbc](https://user-images.githubusercontent.com/60021024/119354240-38ce2c80-bc7a-11eb-85c6-8411e9224e6f.png)
<br>Abra o ODBC Data Sources (32-bit)

![DSN_Sistema](https://user-images.githubusercontent.com/60021024/119354327-57ccbe80-bc7a-11eb-92ec-c54cb42e87c6.png)
<br>Na aba DSN de Sistema, clique em Adicionar.

![informixDriver](https://user-images.githubusercontent.com/60021024/119354361-63b88080-bc7a-11eb-8133-44ed3c9a526d.png)
<br>Na tela Criar nova fonte de dados, selecione INFORMIX ODBC DRIVER e clique em Concluir.

![InformixDsnGeneral](https://user-images.githubusercontent.com/60021024/119354523-8ea2d480-bc7a-11eb-9e60-7de82194e5ff.png)
<br>Na aba General, no campo Data Source Name, digite cms (isso é para assimilar e facilitar a identificação). O campo Description fica em branco.

![InformixDsnConnection](https://user-images.githubusercontent.com/60021024/119354602-a67a5880-bc7a-11eb-9ca4-40951866119c.png)
<br>Na aba Connection, defina os campos da seguinte forma:<br>
<br>
✅Server Name: cms_net <br>
✅Host Name: o seu IP do CMS<br>
✅Service: 50000<br>
✅Protocol: olsoctcp<br>
✅Database Name: cms (este é selecionável em um combobox no campo)<br>
✅User Id: o seu usuário do CMS<br>
✅Password: a sua senha do CMS<br>

![InformixDsnEnvironment](https://user-images.githubusercontent.com/60021024/119354786-d9245100-bc7a-11eb-9c08-488e9f83d246.png)
<br>Na aba Environment, mantenha as configurações padrão que estiverem.<br>
<br>Depois clique em Aplicar e depois em Ok.<br>

