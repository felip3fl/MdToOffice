# TESTE TESTE Análise Financeira da Migração para Oracle Cloud Infrastructure (OCI)

## 1. Custos estimados pós-migração e diferenças financeiras

### 1.1 Cenário atual — On-Premise

A Weyland Corporation possui atualmente:

- **4.000 servidores**
- **700 TB de dados**
- **250 servidores de banco de dados**
- **900 clusters Kubernetes**
- Servidores gerais e Kubernetes:
  - 4 cores
  - 16 GB RAM
- Servidores de banco de dados:
  - 6 cores
  - 32 GB RAM
- Custo mensal aproximado da infraestrutura atual:
  - **R$ 5.000.000,00/mês**

O modelo on-premise exige que a empresa mantenha investimentos e custos relacionados a:

- Servidores físicos;
- Storage;
- Data center;
- Energia elétrica;
- Refrigeração;
- Rede;
- Manutenção;
- Renovação de hardware;
- Equipe de infraestrutura;
- Capacidade excedente para suportar picos de demanda.

---

### 1.2 Cenário proposto — Oracle Cloud Infrastructure

A estimativa mensal para utilização da OCI é:

**R$ 3.688.711,62/mês**

| Indicador | On-Premise | OCI |
|---|---:|---:|
| **Custo mensal** | R$ 5.000.000,00 | R$ 3.688.711,62 |
| Custo anual | R$ 60.000.000,00 | R$ 44.264.539,46 |
| Custo em 3 anos | R$ 180.000.000,00 | R$ 132.793.618,39 |

### 1.3 Economia estimada

A diferença mensal entre os ambientes é:

**R$ 5.000.000,00 − R$ 3.688.711,62 = R$ 1.311.288,38**

Portanto:

- **Economia mensal:** R$ 1.311.288,38
- **Economia anual:** R$ 15.735.460,54
- **Economia em 3 anos:** R$ 47.206.381,61
- **Redução aproximada do custo:** **26,23%**

> A comparação considera os valores mensais fornecidos e assume que permanecem constantes durante os três anos.

---

### 1.4 CAPEX x OPEX

A migração altera significativamente o perfil financeiro da infraestrutura.

#### Modelo On-Premise — maior concentração de CAPEX

O ambiente próprio normalmente exige investimentos antecipados em:

- Servidores;
- Storage;
- Equipamentos de rede;
- Data center;
- Sistemas de energia;
- Refrigeração;
- Expansão da capacidade física.

Esses investimentos são classificados principalmente como **CAPEX**, sendo posteriormente reconhecidos contabilmente por meio da depreciação dos ativos.

#### Modelo OCI — maior concentração de OPEX

Na OCI, a empresa passa a pagar principalmente pelo consumo dos recursos utilizados.

Isso desloca parte significativa dos gastos de:

**CAPEX → OPEX**

O benefício financeiro é reduzir a necessidade de grandes investimentos antecipados em infraestrutura física.

Por outro lado, o OPEX passa a depender diretamente do consumo dos workloads. Portanto, recursos ociosos na nuvem continuam representando custo.

---

### 1.5 Amortização e depreciação

No ambiente on-premise, servidores, storage e equipamentos de infraestrutura representam ativos que normalmente são depreciados ao longo de sua vida útil contábil.

Na migração para OCI, parte desses ativos físicos deixa de ser necessária para sustentar a infraestrutura de produção.

A análise financeira deve considerar:

- Valor contábil atual dos equipamentos;
- Vida útil restante;
- Depreciação acumulada;
- Valor residual;
- Eventuais custos de desmobilização;
- Eventuais perdas contábeis decorrentes da retirada antecipada dos ativos.

Não é possível calcular o valor exato da depreciação ou eventual impacto contábil da migração somente com os dados fornecidos.

---

### 1.6 EBITDA

A mudança para cloud também altera a composição das despesas utilizadas na análise financeira.

O **EBITDA** representa:

> Lucro antes de juros, impostos, depreciação e amortização.

A migração pode reduzir a necessidade de ativos físicos e, consequentemente, alterar a parcela de depreciação e amortização da empresa.

Entretanto, não é possível determinar o EBITDA da Weyland Corporation ou seu impacto exato somente com os custos de infraestrutura apresentados.

Para calcular o impacto financeiro real seriam necessários, no mínimo:

- Receita;
- Custos operacionais;
- Despesas administrativas;
- Depreciação atual;
- Amortização;
- Juros;
- Impostos;
- Demais despesas relacionadas à operação.

---

### 1.7 TCO de 3 anos

Considerando exclusivamente os valores fornecidos:

| Período | On-Premise | OCI | Economia |
|---|---:|---:|---:|
| 1 ano | R$ 60.000.000,00 | R$ 44.264.539,46 | R$ 15.735.460,54 |
| 2 anos | R$ 120.000.000,00 | R$ 88.529.078,92 | R$ 31.470.921,08 |
| 3 anos | R$ 180.000.000,00 | R$ 132.793.618,39 | R$ 47.206.381,61 |

### 1.8 Payback

O payback deve considerar o investimento necessário para realizar a migração.

A economia operacional mensal estimada é:

**R$ 1.311.288,38**

Portanto:

**Payback = Investimento inicial da migração ÷ R$ 1.311.288,38**

Como o investimento inicial de migração não foi informado, não é possível determinar o número exato de meses para o payback.

---

# 2. Pontos de atenção na migração e o que é essencial no primeiro momento

## 2.1 Priorização dos workloads

A migração não deve ocorrer de forma indiscriminada.

Os workloads devem ser classificados considerando:

- Criticidade;
- Dependências;
- Consumo de CPU;
- Consumo de memória;
- Consumo de armazenamento;
- Volume de dados;
- Requisitos de disponibilidade;
- Necessidade de escalabilidade;
- Custo atual.

Uma estratégia adequada é iniciar pelos workloads com menor risco operacional e utilizar os resultados para ajustar as próximas ondas de migração.

---

## 2.2 Inventário financeiro e técnico

Antes da migração, é essencial possuir uma visão consolidada dos:

- 4.000 servidores;
- 250 servidores de banco de dados;
- 900 clusters Kubernetes;
- 700 TB de dados;
- Recursos computacionais utilizados;
- Custos atuais por ambiente;
- Aplicações e suas dependências.

Esse inventário permitirá comparar o **custo atual por workload** com o custo projetado na OCI.

---

## 2.3 Baseline de consumo

Antes da migração, deve ser estabelecido um baseline contendo:

- CPU média;
- CPU de pico;
- Memória média;
- Memória de pico;
- Storage utilizado;
- Tráfego de rede;
- Quantidade de requisições;
- Horários de maior utilização.

Esse baseline será utilizado posteriormente para identificar desperdícios e oportunidades de otimização.

---

## 2.4 CAPEX remanescente

A empresa deve avaliar os equipamentos existentes antes de desativá-los.

É necessário analisar:

- Valor contábil;
- Vida útil restante;
- Depreciação acumulada;
- Contratos de manutenção;
- Contratos de data center;
- Possibilidade de reaproveitamento.

Essa análise evita que a migração gere custos financeiros inesperados relacionados aos ativos existentes.

---

# 3. Como será o uso nesta nova estrutura de Cloud

## 3.1 Modelo de utilização

Na OCI, a infraestrutura será utilizada de forma orientada ao consumo.

Os principais recursos deverão ser dimensionados conforme os workloads existentes:

- Compute para workloads gerais;
- Kubernetes para os clusters;
- Recursos específicos para bancos de dados;
- Storage para os 700 TB de dados;
- Recursos de rede para comunicação entre aplicações;
- Recursos adicionais conforme a demanda das aplicações.

---

## 3.2 Elasticidade

A elasticidade permite alterar a quantidade de recursos disponíveis de acordo com a demanda.

Em períodos de baixa utilização:

- Reduzir recursos;
- Desligar recursos não necessários;
- Redimensionar workloads;
- Utilizar capacidade sob demanda.

Em períodos de alta utilização:

- Aumentar capacidade;
- Escalar workloads;
- Expandir clusters;
- Disponibilizar recursos adicionais temporariamente.

Dessa forma, a empresa evita manter permanentemente capacidade dimensionada apenas para atender aos picos.

---

## 3.3 Investimento conforme demanda

A aquisição de infraestrutura deixa de depender exclusivamente de grandes ciclos de compra de hardware.

A expansão pode ocorrer conforme:

**Demanda → Consumo → Monitoramento → Previsão → Expansão**

Isso permite alinhar o crescimento da infraestrutura ao crescimento real das aplicações.

---

# 4. Cuidados para evitar estourar o orçamento

## 4.1 Governança financeira

A empresa deverá estabelecer uma política de controle de custos da OCI.

Essa política deve contemplar:

- Orçamento mensal;
- Limites de gastos;
- Alertas de orçamento;
- Tags;
- Centros de custo;
- Rateio por área;
- Acompanhamento por aplicação;
- Acompanhamento por ambiente;
- Relatórios financeiros periódicos.

---

## 4.2 Desenvolvendo uma estratégia de otimização de workloads

A estratégia deve seguir um ciclo contínuo:

```text
Medir
  ↓
Analisar
  ↓
Identificar desperdícios
  ↓
Otimizar
  ↓
Medir novamente
```

Cada workload deverá ser analisado considerando sua utilização real.

Exemplos de desperdício:

- CPU subutilizada;
- Memória superdimensionada;
- Storage sem utilização;
- Recursos provisionados para picos que raramente acontecem;
- Ambientes de desenvolvimento ligados continuamente;
- Recursos que não são mais utilizados.

---

## 4.3 Gestão das otimizações de workloads

As otimizações devem possuir responsáveis e acompanhamento.

Uma estrutura de controle pode utilizar:

| Item | Controle |
|---|---|
| Workload | Identificação da aplicação |
| Consumo | CPU, memória, storage e rede |
| Custo | Custo mensal |
| Desperdício | Recursos subutilizados |
| Otimização | Ação proposta |
| Responsável | Equipe responsável |
| Resultado | Economia obtida |

Dessa maneira, a otimização deixa de ser uma ação pontual e passa a fazer parte da gestão financeira da infraestrutura.

---

## 4.4 Compreendendo o valor das oportunidades de otimização

Cada oportunidade deve ser avaliada financeiramente.

Exemplo:

**Custo atual do workload → Custo otimizado → Economia mensal → Economia anual**

Isso permite priorizar as ações que possuem maior impacto financeiro.

---

## 4.5 Previsão de compras e capacidade

Mesmo utilizando cloud, a empresa deve realizar planejamento de capacidade.

A previsão deve considerar:

- Crescimento das aplicações;
- Crescimento dos dados;
- Crescimento dos usuários;
- Histórico de consumo;
- Picos sazonais;
- Novos projetos;
- Expansão da empresa.

O objetivo é evitar dois cenários:

**Subdimensionamento → risco de indisponibilidade**

**Superdimensionamento → desperdício financeiro**

---

# 5. Como manter a demanda das aplicações com a quantidade de recursos disponíveis

## 5.1 Capacity Planning

A capacidade deve ser planejada com base na demanda histórica e projetada.

O processo deve considerar:

```text
Demanda atual
      ↓
Histórico de consumo
      ↓
Previsão de crescimento
      ↓
Capacidade necessária
      ↓
Provisionamento OCI
      ↓
Monitoramento
      ↓
Ajustes
```


---

## 5.3 Estratégia de otimização financeira

A gestão financeira da infraestrutura deve funcionar como um ciclo contínuo:

1. **Medir** o consumo dos workloads.
2. **Identificar** recursos subutilizados.
3. **Calcular** o impacto financeiro.
4. **Priorizar** oportunidades de otimização.
5. **Executar** as otimizações.
6. **Validar** a economia obtida.
7. **Reinvestir** a capacidade financeira em novas demandas.
8. **Reavaliar** continuamente o ambiente.

---

## 5.4 Investimentos para novas demandas

A expansão da infraestrutura deve estar vinculada à demanda real ou projetada.

Em vez de adquirir antecipadamente grande quantidade de infraestrutura física, a empresa pode utilizar o modelo de cloud para:

- Expandir capacidade conforme crescimento;
- Atender novos projetos;
- Aumentar recursos temporariamente;
- Reduzir recursos após períodos de alta demanda;
- Planejar novos investimentos com base no consumo observado.

Isso permite que o crescimento da infraestrutura acompanhe o crescimento das aplicações sem exigir grandes investimentos antecipados em hardware.

---

# Conclusão financeira

Com os valores fornecidos para o cenário, a migração para OCI apresenta a seguinte projeção:

- **Custo atual:** R$ 5.000.000,00/mês
- **Custo estimado OCI:** R$ 3.688.711,62/mês
- **Economia mensal:** R$ 1.311.288,38
- **Economia anual:** R$ 15.735.460,54
- **Economia em 3 anos:** R$ 47.206.381,61
- **Redução estimada:** 26,23%

A gestão financeira após a migração deverá concentrar-se principalmente em **governança de custos, elasticidade, dimensionamento adequado, identificação de desperdícios, otimização contínua dos workloads e planejamento de capacidade**, evitando que o modelo de pagamento por consumo transforme recursos ociosos em novos custos operacionais.
