using Microsoft.VisualBasic;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        string html = """
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {
                    font-family: 'Calibri';
                    font-size: 11pt;
                }

                h1{
                    font-family: 'Calibri Light';
                    font-size: 20pt;
                    font-weight: normal;
                    margin-bottom: 20px;
                    margin-top: 20px;
                }

                h2{
                    font-family: 'Calibri Light';
                    font-size: 20pt;
                    font-style: normal;
                    font-weight: normal;
                    margin-bottom: 20px;
                    margin-top: 20px;
                }

                h3{
                    font-family: 'Calibri';
                    font-size: 16pt;
                    font-style: normal;
                    font-weight: normal;
                    color: #1e4e79;
                    margin-bottom: 20px;
                    margin-top: 20px;
                }

                h4{
                    font-family: 'Calibri';
                    font-size: 14pt;
                    font-style: normal;
                    font-weight: normal;
                    color: #2e75b5;
                    margin-bottom: 20px;
                    margin-top: 20px;
                }
        

                .destaque {
                    background-color: #FFF2CC;
                    padding: 10px;
                    border: 1px solid #D6B656;
                }

                table {
                    border-collapse: collapse;
                    width: 100%;
                }

                th, td {
                    border: 1px solid #999;
                    padding: 6px;
                }

                th {
                    background-color: #D9EAF7;
                }
            </style>
        </head>

        <body>

        <h1 id="an-lise-financeira-e-estrat-gia-de-migra-o-para-oci">Análise Financeira e Estratégia de Migração para OCI</h1>
        <h2 id="1-custos-estimados-p-s-migra-o-e-diferen-as-financeiras">1. Custos estimados pós-migração e diferenças financeiras</h2>
        <p>A Weyland Corporation possui atualmente uma infraestrutura de <strong>4.000 servidores</strong>, com aproximadamente <strong>700 TB de dados</strong>, sendo:</p>
        <ul>
        <li><strong>2.850 servidores de uso geral</strong></li>
        <li><strong>900 servidores em clusters Kubernetes</strong></li>
        <li><strong>250 servidores de banco de dados</strong></li>
        <li>Servidores gerais e Kubernetes: média de <strong>4 vCPUs e 16 GB de RAM</strong></li>
        <li>Servidores de banco de dados: média de <strong>6 vCPUs e 32 GB de RAM</strong></li>
        </ul>
        <p>A proposta é utilizar a <strong>Oracle Cloud Infrastructure (OCI)</strong>, substituindo parte dos investimentos necessários para manter a infraestrutura física por um modelo baseado em consumo de recursos.</p>
        <h3 id="1-1-capex">1.1 CAPEX</h3>
        <p>No ambiente atual, a empresa precisa realizar investimentos de capital para:</p>
        <ul>
        <li>Compra de servidores;</li>
        <li>Expansão de capacidade computacional;</li>
        <li>Armazenamento;</li>
        <li>Equipamentos de rede;</li>
        <li>Data center;</li>
        <li>Sistemas de energia e refrigeração;</li>
        <li>Substituição de equipamentos depreciados.</li>
        </ul>
        <p>Com a migração para OCI, grande parte desses investimentos deixa de ser necessária, pois a infraestrutura física passa a ser responsabilidade do provedor de cloud.</p>
        <p><strong>Trade-off:</strong> a redução de CAPEX é acompanhada pelo aumento da dependência de despesas operacionais relacionadas ao consumo dos serviços de cloud.</p>
        <h3 id="1-2-opex">1.2 OPEX</h3>
        <p>No ambiente cloud, os principais custos passam a estar relacionados ao consumo dos recursos:</p>
        <ul>
        <li>Compute;</li>
        <li>Kubernetes;</li>
        <li>Banco de dados;</li>
        <li>Object Storage;</li>
        <li>Block Storage;</li>
        <li>Rede;</li>
        <li>Backup;</li>
        <li>Monitoramento;</li>
        <li>Segurança;</li>
        <li>Transferência de dados.</li>
        </ul>
        <p>O modelo permite que a empresa pague pelos recursos utilizados e ajuste a capacidade conforme a demanda.</p>
        <p><strong>Trade-off:</strong> o OPEX torna-se mais variável. Um ambiente mal dimensionado ou sem controle financeiro pode gerar aumento significativo da fatura.</p>
        <hr>
        <h2 id="1-3-amortiza-o-e-deprecia-o">1.3 Amortização e depreciação</h2>
        <p>No ambiente físico, servidores e equipamentos de infraestrutura são ativos que sofrem <strong>depreciação</strong> ao longo de sua vida útil.</p>
        <p>Exemplo:</p>
        <ul>
        <li>Servidor adquirido por R$ 50.000;</li>
        <li>Vida útil contábil hipotética de 5 anos;</li>
        <li>Depreciação anual aproximada: R$ 10.000.</li>
        </ul>
        <p>Na OCI, a empresa deixa de adquirir grande parte desses ativos físicos. Portanto, ocorre uma redução da necessidade de registrar depreciação relacionada à infraestrutura própria.</p>
        <p>Os gastos com cloud passam a ser tratados conforme sua natureza contábil e as políticas financeiras da empresa.</p>
        <hr>
        <h2 id="1-4-ebitda">1.4 EBITDA</h2>
        <p>O EBITDA representa o resultado antes de juros, impostos, depreciação e amortização.</p>
        <p>A migração para cloud pode alterar a composição dos custos da empresa porque parte dos custos anteriormente associados à aquisição e depreciação da infraestrutura passa para despesas operacionais de cloud.</p>
        <p>Portanto, <strong>não é correto assumir automaticamente que a migração aumentará ou reduzirá o EBITDA</strong>. O efeito dependerá da estrutura contábil adotada, do custo atual do data center, do custo OCI e da eficiência obtida após a migração.</p>
        <p>Para a análise financeira, devem ser comparados:</p>
        <pre><code class="lang-text">Custo atual de infraestrutura
                <span class="hljs-built_in">x</span>
        Custo operacional da OCI
                <span class="hljs-built_in">x</span>
        Economias obtidas <span class="hljs-keyword">com</span> otimização
        </code></pre>
        <hr>
        <h1 id="1-5-payback">1.5 Payback</h1>
        <p>Para o trabalho, será utilizado um exemplo com <strong>valores fictícios</strong>, conforme solicitado.</p>
        <p>Supondo:</p>
        <table>
        <thead>
        <tr>
        <th>Item</th>
        <th style="text-align:right">Valor fictício</th>
        </tr>
        </thead>
        <tbody>
        <tr>
        <td>Investimento inicial da migração</td>
        <td style="text-align:right">R$ 6.000.000</td>
        </tr>
        <tr>
        <td>Economia anual estimada</td>
        <td style="text-align:right">R$ 2.000.000</td>
        </tr>
        <tr>
        <td>Payback</td>
        <td style="text-align:right">3 anos</td>
        </tr>
        </tbody>
        </table>
        <p>Cálculo:</p>
        <pre><code class="lang-text"><span class="hljs-attr">Payback</span> = Investimento inicial / Economia anual

        <span class="hljs-attr">Payback</span> = R$ <span class="hljs-number">6.000</span>.<span class="hljs-number">000</span> / R$ <span class="hljs-number">2.000</span>.<span class="hljs-number">000</span>

        <span class="hljs-attr">Payback</span> = <span class="hljs-number">3</span> anos
        </code></pre>
        <p>Neste cenário fictício, o investimento realizado para a migração seria recuperado após aproximadamente <strong>3 anos</strong>.</p>
        <p>O cálculo real deverá considerar custos de migração, treinamento, ferramentas, contratos, refatoração das aplicações e economia efetivamente obtida.</p>
        <hr>
        <h1 id="1-6-tco">1.6 TCO</h1>
        <p>O <strong>TCO (Total Cost of Ownership)</strong> deve considerar não apenas o preço dos servidores, mas todos os custos necessários para manter a infraestrutura.</p>
        <h3 id="ambiente-atual">Ambiente atual</h3>
        <pre><code class="lang-text">TCO On-Premises =
        Hardware
        <span class="hljs-bullet">+ </span>Data Center
        <span class="hljs-bullet">+ </span>Energia
        <span class="hljs-bullet">+ </span>Refrigeração
        <span class="hljs-bullet">+ </span>Rede
        <span class="hljs-bullet">+ </span>Licenças
        <span class="hljs-bullet">+ </span>Manutenção
        <span class="hljs-bullet">+ </span>Equipe
        <span class="hljs-bullet">+ </span>Backup
        <span class="hljs-bullet">+ </span>Segurança
        <span class="hljs-bullet">+ </span>Renovação de equipamentos
        </code></pre>
        <h3 id="ambiente-oci">Ambiente OCI</h3>
        <pre><code class="lang-text">TCO OCI =
        Compute
        <span class="hljs-bullet">+ </span>Storage
        <span class="hljs-bullet">+ </span>Database
        <span class="hljs-bullet">+ </span>Kubernetes
        <span class="hljs-bullet">+ </span>Rede
        <span class="hljs-bullet">+ </span>Backup
        <span class="hljs-bullet">+ </span>Segurança
        <span class="hljs-bullet">+ </span>Monitoramento
        <span class="hljs-bullet">+ </span>Suporte
        <span class="hljs-bullet">+ </span>Transferência de dados
        <span class="hljs-bullet">+ </span>Operação
        </code></pre>
        <p>A análise de TCO deverá comparar os custos totais de ambos os modelos durante um período definido, por exemplo, <strong>3 ou 5 anos</strong>, evitando comparar somente o valor mensal da cloud com o preço de aquisição de servidores.</p>
        <hr>
        <h1 id="2-pontos-de-aten-o-na-migra-o">2. Pontos de atenção na migração</h1>
        <p>A migração dos 4.000 servidores deve ser realizada de maneira planejada, evitando uma transferência imediata de todo o ambiente.</p>
        <h2 id="2-1-primeiras-a-es">2.1 Primeiras ações</h2>
        <p>Os pontos essenciais inicialmente são:</p>
        <ul>
        <li>Inventariar os 4.000 servidores;</li>
        <li>Identificar dependências entre aplicações;</li>
        <li>Classificar workloads;</li>
        <li>Identificar aplicações críticas;</li>
        <li>Identificar requisitos de disponibilidade;</li>
        <li>Mapear os 700 TB de dados;</li>
        <li>Avaliar requisitos de segurança;</li>
        <li>Identificar requisitos de licenciamento;</li>
        <li>Medir utilização atual de CPU, memória e armazenamento;</li>
        <li>Criar uma linha de base dos custos atuais.</li>
        </ul>
        <p>A empresa deve evitar simplesmente replicar na OCI a mesma quantidade de recursos existentes no ambiente físico.</p>
        <hr>
        <h2 id="2-2-classifica-o-dos-workloads">2.2 Classificação dos workloads</h2>
        <p>Os workloads podem ser classificados em:</p>
        <table>
        <thead>
        <tr>
        <th>Categoria</th>
        <th>Estratégia</th>
        </tr>
        </thead>
        <tbody>
        <tr>
        <td>Aplicações modernas</td>
        <td>Migrar para serviços cloud</td>
        </tr>
        <tr>
        <td>Kubernetes</td>
        <td>Migrar para OCI Container Engine for Kubernetes</td>
        </tr>
        <tr>
        <td>Bancos de dados</td>
        <td>Avaliar serviços gerenciados e/ou Compute</td>
        </tr>
        <tr>
        <td>Aplicações legadas</td>
        <td>Avaliar migração, modernização ou permanência temporária</td>
        </tr>
        <tr>
        <td>Sistemas pouco utilizados</td>
        <td>Avaliar desligamento ou consolidação</td>
        </tr>
        <tr>
        <td>Dados antigos</td>
        <td>Avaliar armazenamento de menor custo</td>
        </tr>
        </tbody>
        </table>
        <p>Essa classificação permite identificar oportunidades de otimização antes da migração definitiva.</p>
        <hr>
        <h1 id="3-uso-da-nova-estrutura-de-cloud">3. Uso da nova estrutura de cloud</h1>
        <h2 id="3-1-elasticidade">3.1 Elasticidade</h2>
        <p>Uma das principais diferenças da cloud é a capacidade de ajustar recursos conforme a demanda.</p>
        <p>No ambiente físico, a empresa precisa comprar capacidade antecipadamente.</p>
        <p>Na OCI, a capacidade pode ser ajustada conforme o comportamento das aplicações.</p>
        <p>Exemplo:</p>
        <pre><code class="lang-text"><span class="hljs-symbol">Demanda</span> <span class="hljs-keyword">baixa
        </span>     ↓
        <span class="hljs-symbol">Menos</span> recursos
             ↓
        <span class="hljs-symbol">Menor</span> consumo
             ↓
        <span class="hljs-symbol">Menor</span> custo
        </code></pre>
        <p>Durante períodos de alta demanda:</p>
        <pre><code class="lang-text">Aumento da demanda
               ↓
        Aumento <span class="hljs-keyword">dos</span> recursos
               ↓
        Atendimento da aplicação
               ↓
        Redução <span class="hljs-keyword">dos</span> recursos após o pico
        </code></pre>
        <p>Essa característica reduz a necessidade de manter capacidade física permanentemente ociosa.</p>
        <hr>
        <h1 id="3-2-previs-o-de-compras">3.2 Previsão de compras</h1>
        <p>Na cloud, a previsão deixa de ser baseada principalmente na compra de servidores e passa a ser baseada na previsão de consumo.</p>
        <p>A equipe deve acompanhar:</p>
        <ul>
        <li>Crescimento de usuários;</li>
        <li>Crescimento de dados;</li>
        <li>CPU;</li>
        <li>Memória;</li>
        <li>Storage;</li>
        <li>Número de workloads;</li>
        <li>Crescimento dos clusters Kubernetes;</li>
        <li>Crescimento dos bancos de dados;</li>
        <li>Custos mensais.</li>
        </ul>
        <p>A previsão deve ser realizada em conjunto pelas equipes de <strong>TI, FinOps e Financeiro</strong>.</p>
        <hr>
        <h1 id="3-3-investimentos-para-demandas">3.3 Investimentos para demandas</h1>
        <p>Os investimentos devem ser direcionados para as necessidades reais do negócio.</p>
        <p>Exemplo:</p>
        <pre><code class="lang-text">Nova aplicação
              ↓
        Estimativa <span class="hljs-keyword">de</span> demanda
              ↓
        Estimativa <span class="hljs-keyword">de</span> recursos OCI
              ↓
        Estimativa <span class="hljs-keyword">de</span> custo
              ↓
        Aprovação financeira
              ↓
        Provisionamento
        </code></pre>
        <p>Isso evita provisionar recursos antes que exista uma necessidade real.</p>
        <hr>
        <h1 id="4-controle-e-otimiza-o-financeira">4. Controle e otimização financeira</h1>
        <p>A Weyland deve implementar uma estratégia de <strong>FinOps</strong> para controlar o consumo da OCI.</p>
        <p>O objetivo é estabelecer um ciclo contínuo:</p>
        <pre><code class="lang-text">Visibilidade
             ↓
        <span class="hljs-keyword">An</span>álise
             ↓
        Otimização
             ↓
        Controle
             ↓
        Nova <span class="hljs-keyword">an</span>álise
        </code></pre>
        <hr>
        <h2 id="4-1-estrat-gia-de-otimiza-o-de-workloads">4.1 Estratégia de otimização de workloads</h2>
        <p>Cada workload deve ser analisado considerando:</p>
        <ul>
        <li>Utilização de CPU;</li>
        <li>Utilização de memória;</li>
        <li>Armazenamento;</li>
        <li>I/O;</li>
        <li>Rede;</li>
        <li>Horários de utilização;</li>
        <li>Crescimento esperado;</li>
        <li>Criticidade;</li>
        <li>Custo mensal.</li>
        </ul>
        <p>Workloads subutilizados devem ser redimensionados.</p>
        <p>Exemplo:</p>
        <pre><code class="lang-text">Recurso <span class="hljs-string">provisionado:</span>
        <span class="hljs-number">4</span> vCPU / <span class="hljs-number">16</span> GB

        Utilização mé<span class="hljs-string">dia:</span>
        <span class="hljs-number">1</span> vCPU / <span class="hljs-number">4</span> GB

        Açã<span class="hljs-string">o:</span>
        Redimensionamento do workload
        </code></pre>
        <p>Isso evita pagar continuamente por recursos que não estão sendo utilizados.</p>
        <hr>
        <h1 id="4-2-gest-o-das-otimiza-es-de-workloads">4.2 Gestão das otimizações de workloads</h1>
        <p>A otimização deve ser tratada como um processo contínuo.</p>
        <h3 id="processo">Processo</h3>
        <ol>
        <li>Identificar recursos subutilizados.</li>
        <li>Calcular o custo atual.</li>
        <li>Identificar a possível redução.</li>
        <li>Avaliar impacto na aplicação.</li>
        <li>Realizar a alteração.</li>
        <li>Monitorar o resultado.</li>
        <li>Registrar a economia obtida.</li>
        </ol>
        <p>Cada otimização deve possuir um responsável e uma meta de economia.</p>
        <hr>
        <h1 id="4-3-compreendendo-o-valor-das-oportunidades">4.3 Compreendendo o valor das oportunidades</h1>
        <p>As oportunidades devem ser priorizadas considerando:</p>
        <pre><code class="lang-text">Economia potencial
                +
        Facilidade de implementação
                +
        Risco da mudanç<span class="hljs-selector-tag">a</span>
                +
        Impacto na aplicação
        </code></pre>
        <p>Exemplo:</p>
        <table>
        <thead>
        <tr>
        <th>Oportunidade</th>
        <th style="text-align:right">Economia potencial</th>
        <th>Risco</th>
        </tr>
        </thead>
        <tbody>
        <tr>
        <td>Redimensionar Compute subutilizado</td>
        <td style="text-align:right">Alta</td>
        <td>Baixo</td>
        </tr>
        <tr>
        <td>Desligar ambientes fora do horário</td>
        <td style="text-align:right">Média</td>
        <td>Baixo</td>
        </tr>
        <tr>
        <td>Otimizar Storage</td>
        <td style="text-align:right">Média</td>
        <td>Baixo</td>
        </tr>
        <tr>
        <td>Alterar arquitetura de aplicação</td>
        <td style="text-align:right">Alta</td>
        <td>Médio/Alto</td>
        </tr>
        <tr>
        <td>Migrar aplicação legada</td>
        <td style="text-align:right">Variável</td>
        <td>Alto</td>
        </tr>
        </tbody>
        </table>
        <p>Dessa maneira, a equipe consegue concentrar esforços nas otimizações que possuem maior impacto financeiro e menor risco operacional.</p>
        <hr>
        <h1 id="5-manuten-o-da-demanda-das-aplica-es">5. Manutenção da demanda das aplicações</h1>
        <p>A quantidade de recursos deve ser determinada pelo <strong>consumo real das aplicações</strong>, e não simplesmente pelo tamanho atual do ambiente físico.</p>
        <p>Para isso, devem ser utilizados mecanismos de monitoramento e métricas de utilização.</p>
        <h2 id="5-1-dimensionamento-baseado-em-demanda">5.1 Dimensionamento baseado em demanda</h2>
        <p>A equipe deve estabelecer limites para:</p>
        <ul>
        <li>CPU;</li>
        <li>Memória;</li>
        <li>Storage;</li>
        <li>IOPS;</li>
        <li>Rede;</li>
        <li>Número de instâncias;</li>
        <li>Capacidade dos clusters Kubernetes.</li>
        </ul>
        <p>Quando a demanda aumentar, a infraestrutura poderá ser expandida.</p>
        <p>Quando a demanda diminuir, os recursos poderão ser reduzidos.</p>
        <hr>
        <h1 id="5-2-redu-o-do-desperd-cio">5.2 Redução do desperdício</h1>
        <p>Os principais desperdícios que devem ser combatidos são:</p>
        <ul>
        <li>Servidores superdimensionados;</li>
        <li>Instâncias sem utilização;</li>
        <li>Recursos provisionados e não utilizados;</li>
        <li>Ambientes de desenvolvimento funcionando 24 horas;</li>
        <li>Storage sem necessidade;</li>
        <li>Dados mantidos em armazenamento de alto custo;</li>
        <li>Recursos duplicados;</li>
        <li>Capacidade reservada sem demanda;</li>
        <li>Falta de acompanhamento dos custos.</li>
        </ul>
        <p>Uma estratégia simples é utilizar o seguinte ciclo:</p>
        <pre><code class="lang-text"><span class="hljs-attribute">MEDIR</span>
          ↓
        ANALISAR
          ↓
        REDIMENSIONAR
          ↓
        MONITORAR
          ↓
        ECONOMIZAR
        </code></pre>
        <hr>
        <h1 id="conclus-o">Conclusão</h1>
        <p>A migração dos 4.000 servidores para a <strong>Oracle Cloud Infrastructure</strong> altera principalmente a estrutura financeira da Weyland Corporation. O modelo reduz a necessidade de investimentos antecipados em infraestrutura física, mas aumenta a importância do controle do OPEX e do consumo dos serviços cloud.</p>
        <p>Para evitar aumento desnecessário dos custos, a empresa deve combinar <strong>elasticidade, FinOps, monitoramento, dimensionamento adequado e otimização contínua dos workloads</strong>.</p>
        <p>O principal objetivo financeiro deve ser garantir que a quantidade de recursos consumidos acompanhe a <strong>demanda real das aplicações</strong>, reduzindo capacidade ociosa e evitando desperdícios.</p>
        

        </body>
        </html>
        """;

        CopiarHtmlParaClipboard(html);

        Console.WriteLine("HTML copiado para o Clipboard.");
        Console.WriteLine("Abra o Word e pressione Ctrl+V.");
    }

    static void CopiarHtmlParaClipboard(string html)
    {
        string clipboardHtml = CriarClipboardHtml(html);

        var data = new DataObject();

        // HTML para aplicações como Word
        data.SetData(DataFormats.Html, clipboardHtml);

        // Texto alternativo
        data.SetData(
            DataFormats.Text,
            RemoverTagsHtml(html)
        );

        Clipboard.SetDataObject(data, true);
    }

    static string CriarClipboardHtml(string html)
    {
        const string header =
            "Version:0.9\r\n" +
            "StartHTML:{0:D10}\r\n" +
            "EndHTML:{1:D10}\r\n" +
            "StartFragment:{2:D10}\r\n" +
            "EndFragment:{3:D10}\r\n";

        const string startFragment =
            "<!--StartFragment-->";

        const string endFragment =
            "<!--EndFragment-->";

        string body =
            "<html><body>" +
            startFragment +
            html +
            endFragment +
            "</body></html>";

        int headerLength = string.Format(
            header,
            0,
            0,
            0,
            0
        ).Length;

        int startHtml = headerLength;

        int endHtml = startHtml + body.Length;

        int startFragmentPosition =
            startHtml +
            body.IndexOf(startFragment) +
            startFragment.Length;

        int endFragmentPosition =
            startHtml +
            body.IndexOf(endFragment);

        string result = string.Format(
            header,
            startHtml,
            endHtml,
            startFragmentPosition,
            endFragmentPosition
        ) + body;

        return result;
    }

    static string RemoverTagsHtml(string html)
    {
        return System.Text.RegularExpressions.Regex
            .Replace(html, "<.*?>", string.Empty);
    }
}