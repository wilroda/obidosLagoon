Workshop Script

* Apresentação
  * O que é que eles sabem
  * Introdução ao 3d
    * Mostrar um modelo
      * opções de importação por alto
    * Materiais
    * Rotações
    * Importância de hierarquias * rotação em torno de um eixo * mostrar uma porta com uma fence e um cubo para fechar
    * Animator com 3d (opcional)
    * Iluminação
* Workshop
  * Mostrar uma build
  * Criar uma cena nova
    * Overview dos componentes, correr a cena
    * Meter a cena de noite
      * Ajustar a luz directa
    * Meter uma fogueira com uma luz
    * Meter mais uns objectos
      * Explicar snap to grid
      * Explicar snap to vertex
    * Mostrar o nevoeiro
  * Explicar o sistema de acções
    * ShowTooltip * Elefante diz para se carregar nele
    * PlaySound * Elefante faz som
    * Talk * Elefante diz algo
  * Explicar tokens
  * Explicar condições de tokens
    * GiveToken * Elefante só fala quando não temos o token e dá os tokens
    * RemoveToken * Carregamos num botão e ficamos sem o token
  * Explicar sistema de quests
    * GiveQuest
  * Explicar condições de quests * elefante só dá a quest uma vez
  * Explicar eventos
    * OnQuestCompleted
      * HideObject
    * OnQuestFailed
      * GameOver
  * Acção Move
    * Começar por mostrar com um coelho, o bounce
    * Mostrar o modo automático
  * Explicar o botão que faz o coelho mover
    * Redirect * Mostrar um botão que faz abrir um alçapão
    * Meter uma cenoura que caiu do alçapão
    * OnDistance * Meter a cenoura a cair no chão no alcance do coelho e ele move-se para lá
    * Quando ele apanha a cenoura o jogo acaba
      * QuitApplication
      * ChangeLevel * Voltar ao menu principal
  * Tópicos avançados
    * Chest com moeda lá dentro
      * PlayAnimation
    * Animator (se não dei mais atrás)
    * Efeitos especiais
      * Fogo
      * Explosão
      * Água