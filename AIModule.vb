Module AIModule
    'Strategy index
    '0=Spy(1)
    '1=Scout(2)
    '2=Miner(3)
    '3=Sergeant(4)
    '4=Lieutenant(5)
    '5=Captain(6)
    '6=Major(7)
    '7=Colonel(8)
    '8=General(9)
    '9=Marshall(10)
    '10=Bomb(B)
    '11=Flag(F)
    'To access this table: SuitabilityTable(0         , 2             , 0                  )
    '                                       My piece-1, Player piece-1, 0=Hidden 1=Revealed
    Dim SuitabilityTable(,,) As Integer = {{{0, 0}, {-54, -55}, {-45, -55}, {-53, -55}, {-50, -55}, {-45, -55}, {-41, -55}, {-38, -55}, {-15, -55}, {600, 600}, {-30, Nothing}, {900, Nothing}},
                                           {{200, 200}, {0, 0}, {100, -10}, {20, -10}, {30, -10}, {100, -10}, {110, -10}, {125, -10}, {150, -10}, {200, -10}, {150, Nothing}, {1000, Nothing}},
                                           {{175, 175}, {100, 100}, {0, 0}, {-60, -70}, {-55, -70}, {-45, -70}, {-30, -70}, {-20, -70}, {-15, -70}, {0, -70}, {600, Nothing}, {950, Nothing}},
                                           {{150, 150}, {70, 70}, {210, 210}, {0, 0}, {-15, -20}, {0, -20}, {40, -20}, {45, -20}, {75, -20}, {100, -20}, {75, Nothing}, {970, Nothing}},
                                           {{100, 100}, {30, 30}, {175, 175}, {125, 125}, {0, 0}, {-45, -50}, {-35, -50}, {-15, -50}, {0, -50}, {40, -50}, {25, Nothing}, {950, Nothing}},
                                           {{50, 50}, {0, 0}, {150, 150}, {75, 75}, {150, 150}, {0, 0}, {-80, -100}, {-60, -100}, {-34, -100}, {0, -100}, {0, Nothing}, {940, Nothing}},
                                           {{25, 25}, {-5, -5}, {100, 100}, {20, 20}, {100, 100}, {20, 20}, {0, 0}, {-90, -140}, {-74, -140}, {-40, -140}, {-25, Nothing}, {930, Nothing}},
                                           {{15, 15}, {-10, -10}, {70, 70}, {0, 0}, {50, 50}, {150, 150}, {255, 225}, {0, 0}, {-100, -175}, {-75, -175}, {-100, Nothing}, {920, Nothing}},
                                           {{5, 5}, {-30, -30}, {50, 50}, {-10, -10}, {20, 20}, {100, 100}, {175, 175}, {250, 250}, {0, 0}, {-250, -300}, {-150, Nothing}, {910, Nothing}},
                                           {{40, 40}, {-100, -100}, {40, 40}, {-20, -20}, {10, 10}, {50, 50}, {140, 140}, {200, 200}, {300, 300}, {0, 0}, {-200, Nothing}, {900, Nothing}}}
    Dim PieceValues() As Integer = {55, 10, 100, 20, 50, 100, 140, 15, 300, 400, 75, 1000}
    'Spy loses to first line, scout loses to second line, miner loses to third line...
    Dim WhoLoses()() As Integer = {New Integer() {Piece.Spy, Piece.Scout, Piece.Miner, Piece.Sergeant, Piece.Lieutenant, Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Bomb},
                                  New Integer() {Piece.Scout, Piece.Miner, Piece.Sergeant, Piece.Lieutenant, Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Miner, Piece.Sergeant, Piece.Lieutenant, Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall},
                                  New Integer() {Piece.Sergeant, Piece.Lieutenant, Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Lieutenant, Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Spy, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Spy, Piece.Scout, Piece.Miner, Piece.Sergeant, Piece.Lieutenant, Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb},
                                  New Integer() {Piece.Spy, Piece.Scout, Piece.Miner, Piece.Sergeant, Piece.Lieutenant, Piece.Captain, Piece.Major, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb}}
    Dim IsAIRedTeam As Boolean = False
    Dim PieceFreq As List(Of Integer) = New List(Of Integer) From {Piece.Spy, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Scout, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Miner, Piece.Sergeant, Piece.Sergeant, Piece.Sergeant, Piece.Sergeant, Piece.Lieutenant, Piece.Lieutenant, Piece.Lieutenant, Piece.Lieutenant, Piece.Captain, Piece.Captain, Piece.Captain, Piece.Captain, Piece.Major, Piece.Major, Piece.Major, Piece.Colonel, Piece.Colonel, Piece.General, Piece.Marshall, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Bomb, Piece.Flag} 'List every piece. This list is then re-used to keep track of the pieces that the player still has hidden.
    Dim PieceHistories(39) As PieceHistory
    Dim OldBoard As BoardState

    Function Main(ByVal GameInfo As InfoPacket) As Move 'Plays the main game
        Randomize() 'Important for tree building
        'Recount Pieces
        PieceFreq = PieceFrequencies.ToList()
        For Each Piece In GameInfo.Graveyard
            If Piece.IsRed = IsAIRedTeam Then 'On our team
                PieceFreq.Remove(Piece.Value) 'Remove the corresponding value.
            End If
        Next

        'Log what happened into the piecehistories
        If Not IsNothing(GameInfo.PreviousBoard) Then
            LogEventsForUs(GameInfo.PreviousBoard, GameInfo.PreviousPlayerMove)
        End If

        'Find best board
        Dim Depth As Integer = 2 'The depth
        Dim StartNode As Node = New MaxNode(GameInfo.MyBoard) 'The current situation
        Dim MovesList As List(Of Move) = FindChildrenMoves(StartNode) 'Build first level
        Dim MaxValue As Decimal = Decimal.MinValue 'This is used for finding the highest value
        Dim BestNodeIndex = 0 'Find the index we are looking for
        For i = 0 To StartNode.ChildNodes.Count - 1
            Dim Child As Node = StartNode.ChildNodes(i)
            If Child.GetType = GetType(ChanceNode) Then
                For j = 0 To Child.ChildNodes.Count - 1
                    BuildTree(Child.ChildNodes(j), Depth - 1) 'Finds the rest of the tree
                Next
            Else
                BuildTree(Child, Depth - 1) 'Finds the rest of the tree
            End If
            Dim ExpectiminimaxValue As Decimal = Expectiminimax(Child, Depth - 1)
            If ExpectiminimaxValue > MaxValue Then 'If we found a better board
                MaxValue = ExpectiminimaxValue 'Save the details of this board
                BestNodeIndex = i
            End If
        Next
        'Find move
        Dim AIMove As Move = MovesList(BestNodeIndex)
        'Send off the move
        Return MakeMove(GameInfo, AIMove)
    End Function

    Sub EndGameAnalysis()
        Dim MyStrategies() As Strategy = LoadStrategies() 'Strategies in here are ordered as indicated at the top
        For Each MyPieceHistory As PieceHistory In PieceHistories 'Look at all that happened

            If MyPieceHistory.PiecesDestroyed.Count > 0 Then 'If the list is 0 then we can ignore this. Each piece is just at good at doing nothing

                Dim MyHistoryScore = GiveHistoryPoints(MyPieceHistory) 'The current score
                Dim BestPieceNumber As Integer 'Stores the piece value of the best score. Assume this is the current one
                Dim BestHistoryScore As Integer = Integer.MinValue 'Finds the best score

                For i = Piece.Spy To Piece.Flag 'Look through the pieces
                    Dim TestPiece As BoardPiece = New BoardPiece(i, IsAIRedTeam) 'Piece to try out
                    Dim MyNewHistory As PieceHistory = MyPieceHistory
                    MyNewHistory.PieceType = TestPiece
                    Dim TempScore As Integer = GiveHistoryPoints(MyNewHistory)
                    If TempScore > BestHistoryScore Then 'We found a better score
                        BestHistoryScore = TempScore
                        BestPieceNumber = i
                    End If
                Next
                'Regret calculation.
                Dim Regret As Integer = BestHistoryScore - MyHistoryScore
                MyStrategies(BestPieceNumber - 1).AddRegret(MyPieceHistory.StartPosition, Regret) 'Regret
            End If
        Next
        SaveStrategies(MyStrategies)
    End Sub

    Function GiveHistoryPoints(ByVal MyPieceHistory As PieceHistory) As Integer 'Rates the history
        Dim Suitability As Integer = 0 'Stores the suitability of the piece, given this piece's history.
        For Each PlayerPiece In MyPieceHistory.PiecesDestroyed
            If PlayerPiece.Hidden Then
                Suitability += SuitabilityTable(MyPieceHistory.PieceType.Value - 1, PlayerPiece.Value - 1, 1)
            Else
                Suitability += SuitabilityTable(MyPieceHistory.PieceType.Value - 1, PlayerPiece.Value - 1, 0)
            End If
        Next
        Return Suitability
    End Function

    Function Evaluate(ByVal GameBoard As BoardState) 'Gives a score on how good a board is.
        Dim ValueOfBoard As Decimal = 0 'The score of the board
        Dim MyPieces As New List(Of PieceLocation)
        Dim PlayerPieces As New List(Of PieceLocation)

        Dim MyProbabilities As Double() = GenerateProbabilityMatrix()
        Dim AveragePlayerPieceValue As Decimal = 0
        For PValue = 0 To 11 'Get average of player piece
            AveragePlayerPieceValue += PieceValues(PValue) * MyProbabilities(PValue)
        Next

        For x = 0 To BoardSide - 1
            For y = 0 To BoardSide - 1 'Scrolling through the pieces
                Dim SelectedPiece As BoardPiece = GameBoard.Pieces(x, y)
                If Not IsNothing(SelectedPiece) Then 'Piece found
                    Dim MyPieceLocal As PieceLocation = New PieceLocation(SelectedPiece, New VectorInt(x, y))
                    If SelectedPiece.IsRed = IsAIRedTeam Then 'On my team
                        MyPieces.Add(MyPieceLocal)
                        ValueOfBoard += PieceValues(MyPieceLocal.MyPiece.Value - 1) 'Store how much AI piece is worth
                    Else 'Enemy piece
                        PlayerPieces.Add(MyPieceLocal)
                        If CantSee(MyPieceLocal.MyPiece) Then
                            ValueOfBoard -= AveragePlayerPieceValue 'Subtract material value
                        Else
                            ValueOfBoard -= PieceValues(MyPieceLocal.MyPiece.Value - 1)
                        End If
                    End If
                End If
            Next
        Next

        For Each AIPieceL In MyPieces 'Go through the pieces we found.
            Dim AiPieceValue As Double = PieceValues(AIPieceL.MyPiece.Value - 1) 'Store how much AI piece is worth

            For Each HumanPieceL In PlayerPieces
                Dim xDistance As Integer = Math.Abs(HumanPieceL.MyLocation.x - AIPieceL.MyLocation.x) 'Calculate approximate distance.
                Dim yDistance As Integer = Math.Abs(HumanPieceL.MyLocation.y - AIPieceL.MyLocation.y)
                Dim Distance As Integer = xDistance + yDistance

                Dim AIPiece As BoardPiece = AIPieceL.MyPiece
                Dim HumanPiece As BoardPiece = HumanPieceL.MyPiece

                If HumanPieceL.MyPiece.Hidden = True Then 'If it's hidden then use probabilities
                    Dim ValueToAdd As Decimal = 0 'This is the value we will add after working through the probabilities.

                    Dim AIPieceLosesTo As Integer() = WhoLoses(AIPiece.Value - 1) 'All the pieces that the AI piece loses to.

                    For PretendHumanPiece = 1 To 12 'Loop through all possible pieces the human could be
                        ValueToAdd = 0 'Clear value
                        Dim HumanPieceValue As Integer = PieceValues(PretendHumanPiece - 1)
                        Dim ProbabilityOfPiece As Decimal = MyProbabilities(PretendHumanPiece - 1)
                        If AIPieceLosesTo.Contains(PretendHumanPiece) Then 'If the AI piece loses
                            ValueToAdd -= (AIPieceValue) / Distance 'Lose the value of the piece
                        Else
                            ValueToAdd += HumanPieceValue / Distance 'Gain value of piece we might capture
                        End If
                        ValueToAdd = ValueToAdd * ProbabilityOfPiece 'Scale for probability
                        ValueOfBoard += ValueToAdd 'Add on
                    Next

                Else 'Don't use probability
                    Dim HumanPieceValue As Integer = PieceValues(HumanPiece.Value - 1)
                    Dim AIPieceLosesTo As Integer() = WhoLoses(AIPiece.Value - 1) 'All the pieces that the AI piece loses to.
                    If AIPieceLosesTo.Contains(HumanPiece.Value) Then 'If the AI piece loses
                        ValueOfBoard -= (AIPieceValue) / Distance 'Lose the value of the piece
                    Else
                        ValueOfBoard += HumanPieceValue / Distance 'Gain value of piece we might capture
                    End If
                End If
            Next
        Next

        Return ValueOfBoard
    End Function

    Function MakeMove(ByVal GameInfo As InfoPacket, ByVal MyMove As Move) As Move
        'Log AI move.
        Dim StartLocation As VectorInt = MyMove.StartVector 'Where the AI is moving from
        Dim EndLocation As VectorInt = MyMove.EndVector 'Where the AI is moving to
        Dim MyPiece As BoardPiece = GameInfo.MyBoard.Pieces(StartLocation.x, StartLocation.y) 'Get piece that is moving
        Dim PieceIndex As Integer = FindPieceHistoryAt(StartLocation) 'Find AI piece
        If Not IsNothing(MyPiece) Then 'No need to check if it's AI piece. If the player is moving onto a piece, it can't be his own, it must be an AI piece.
            PieceHistories(PieceIndex).PiecesDestroyed.Add(GameInfo.MyBoard.Pieces(EndLocation.x, EndLocation.y)) 'Note: The AI running into a piece, and a piece running into the AI are equivalent in the eyes of my model.
        End If
        'Update position
        PieceHistories(PieceIndex).CurrentPosition = EndLocation 'Update position
        Return (MyMove)
    End Function

    Function FindPieceHistoryAt(ByVal Location As VectorInt) As Integer 'Find the index of a piece history
        Dim PieceIndex As Integer = 0 'Store the location of the correct PieceHistory
        Dim SearchFound As Boolean = False 'Boolean for is the search is over
        While PieceIndex < PieceHistories.Length AndAlso SearchFound = False
            If PieceHistories(PieceIndex).AmIHere(Location) Then 'Check if the piece is at the end location
                SearchFound = True 'We found it, break out of loop
            Else
                PieceIndex += 1 'If it wasn't found at this index, search the next index
            End If
        End While
        If SearchFound = False Then 'If it is still false, something has gone wrong. The program shouldn't continue.
            Throw New Exception("Search didn't find anything.")
        End If
        Return PieceIndex
    End Function

    Sub LogEventsForUs(ByVal OldBoard As BoardState, ByVal PlayerMove As Move) 'Logs events to update our piecehistories.
        Dim EndLocation As VectorInt = PlayerMove.EndVector 'Where the player moved to
        Dim MyPiece As BoardPiece = OldBoard.Pieces(EndLocation.x, EndLocation.y) 'Get piece being attacked
        If Not IsNothing(MyPiece) Then 'No need to check if it's AI piece. If the player is moving onto a piece, it can't be his own, it must be an AI piece.
            Dim PieceIndex As Integer = FindPieceHistoryAt(EndLocation)
            PieceHistories(PieceIndex).PiecesDestroyed.Add(OldBoard.Pieces(PlayerMove.StartVector.x, PlayerMove.StartVector.y)) 'Note: The player running into a piece, and the piece running into the player are equivalent in the eyes of my model.
        End If
    End Sub

    Function Expectiminimax(ByVal MyNode As Node, ByVal Depth As Integer) As Double 'Evaluates a given node
        Dim NodeType As Type = MyNode.GetType() 'Get the type of our child node
        Dim Alpha As Double
        If NodeType = GetType(ChanceNode) Then
            Dim MyChanceNode As ChanceNode = MyNode 'You can't access probabilities from MyNode since it's not necessarily a node.

            'Return weighted average of all child nodes' values
            Alpha = 0
            For i = 0 To MyChanceNode.ChildNodes.Count - 1
                Alpha += MyChanceNode.MyProbabilities(i) * Expectiminimax(MyChanceNode.ChildNodes(i), Depth)
            Next
        ElseIf MyNode.ChildNodes.Count = 0 OrElse Depth = 0 Then
            Dim Evaluation As Double = Evaluate(MyNode.MyBoardState)

            Return Evaluation
        ElseIf NodeType = GetType(MinNode) Then
            'Return value of minimum valued child node
            Alpha = Double.MaxValue
            For i = 0 To MyNode.ChildNodes.Count - 1
                Alpha = Min(Alpha, Expectiminimax(MyNode.ChildNodes(i), Depth - 1))
            Next
        ElseIf NodeType = GetType(MaxNode) Then
            'Return value of maximum valued child node
            Alpha = Double.MinValue
            For i = 0 To MyNode.ChildNodes.Count - 1
                Alpha = Max(Alpha, Expectiminimax(MyNode.ChildNodes(i), Depth - 1))
            Next
        End If
        Return Alpha
    End Function

    Function BuildTree(ByRef MyNode As Node, ByVal Depth As Integer) As Node 'Takes a node and returns that node with all it's children
        FindChildren(MyNode)
        If Depth = 1 OrElse MyNode.ChildNodes.Count = 0 Then 'The depth is 0 or the node is terminal
            Return MyNode
        Else 'Add it's child nodes
            For i = 0 To MyNode.ChildNodes.Count - 1
                Dim NodeType As Type = MyNode.ChildNodes(i).GetType() 'Get the type of our child node

                If NodeType = GetType(MaxNode) OrElse NodeType = GetType(MinNode) Then 'If it's min or max
                    MyNode.ChildNodes(i) = BuildTree(MyNode.ChildNodes(i), Depth - 1) 'TODO: Optimise this.
                Else 'It must be a chance node
                    For j = 0 To MyNode.ChildNodes(i).ChildNodes.Count - 1 'Go through all the child nodes
                        MyNode.ChildNodes(i).ChildNodes(j) = BuildTree(MyNode.ChildNodes(i).ChildNodes(j), Depth - 1)
                    Next
                End If
            Next
        End If
        Return MyNode 'Return the new node
    End Function

    Sub SaveStrategies(ByVal MyStrats As Strategy())
        Dim MyDirectoy As String = System.IO.Directory.GetCurrentDirectory.ToString() + "\AIDATA.bin" 'This is the debug\bin folder.
        BinaryFileSave.SaveIntoBinary(MyStrats, MyDirectoy)
    End Sub

    Function LoadStrategies() As Strategy()
        Dim ReturnStrats() As Strategy
        Dim MyDirectoy As String = System.IO.Directory.GetCurrentDirectory.ToString() + "\AIDATA.bin" 'This is the debug\bin folder.
        If My.Computer.FileSystem.FileExists(MyDirectoy) Then
            ReturnStrats = BinaryFileSave.LoadToObject(MyDirectoy)
            If ReturnStrats.Length = 12 Then 'There must be 12 strategies, one for each piece
                Return ReturnStrats
            End If
        End If
        'This is the default value
        ReturnStrats = New Strategy() {New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy(), New Strategy()}
        Return ReturnStrats
    End Function

    Function GetPlacement(ByVal AIRedTeam As Boolean) As BoardPiece(,) 'Must be a 10 by 4 array that stores parameters.
        IsAIRedTeam = AIRedTeam
        PieceFreq = Utils.PieceFrequencies.ToList()
        Dim MyStrategies() As Strategy = LoadStrategies() 'Strategies in here are ordered as indicated at the top of the page.

        Dim StartPieces(9, 3) As BoardPiece

        Dim FlagPostion As VectorInt
        PlaceFlag(StartPieces, MyStrategies, FlagPostion)

        PlaceRelativeToFlag(StartPieces, MyStrategies, FlagPostion)

        SaveStrategies(MyStrategies)

        Return StartPieces
    End Function

    Sub PlaceFlag(ByRef BoardArray(,) As BoardPiece, ByVal Strategies() As Strategy, ByRef FlagPos As VectorInt) 'Places flag
        FlagPos = Strategies(11).PickOne() 'Get strategy to choose a position.
        Dim MyFlag As BoardPiece = New BoardPiece(12, IsAIRedTeam)
        BoardArray(FlagPos.x, FlagPos.y) = MyFlag 'Put piece in board array.
        PieceHistories(39) = New PieceHistory(FlagPos, MyFlag) 'Record where the piece was placed.
    End Sub

    Function GetAllScoutPositions(ByVal MyPiecePos As VectorInt, ByVal Board As BoardState) As List(Of VectorInt) 'Generates a list of all the possible places that the scout can go.
        Dim ReturnList As New List(Of VectorInt)
        Dim NorthEastSouthWest() As VectorInt = {New VectorInt(1, 0), New VectorInt(0, 1), New VectorInt(-1, 0), New VectorInt(0, -1)} 'Stores the 4 positions that have to be checked for each piece.
        Dim PlaceToAdd As VectorInt
        For i = 0 To 3
            PlaceToAdd = MyPiecePos
            Do
                PlaceToAdd = PlaceToAdd.Add(NorthEastSouthWest(i)) 'Keep adding
                ReturnList.Add(PlaceToAdd)
            Loop Until Not InBounds(PlaceToAdd) OrElse Not IsNothing(Board.Pieces(PlaceToAdd.x, PlaceToAdd.y))
            'Until either the lakes, out of bounds, or a piece stops you. It doesn't matter if one of the pieces is out of bounds.
        Next

        Return ReturnList
    End Function

    Sub FindChildren(ByRef MyNode As Node) 'Finds the children of a node and adds them(Only 1 level deep)
        Dim WorkingBoard As BoardState = MyNode.MyBoardState 'The board we are working with.
        Dim MyProbabilityMatrix As Double() = GenerateProbabilityMatrix()
        For x = 0 To Utils.BoardSide - 1
            For y = 0 To Utils.BoardSide - 1 'Look at every piece
                If Not IsNothing(WorkingBoard.Pieces(x, y)) AndAlso WorkingBoard.Pieces(x, y).IsRed = MyNode.MyBoardState.IsRedToPlay Then 'The piece must meet these coditions

                    Dim MyPiece As BoardPiece = WorkingBoard.Pieces(x, y)
                    If MyPiece.Value = 0 Then
                        Console.ReadLine()
                    End If
                    Dim NorthEastSouthWest() As VectorInt = {New VectorInt(1, 0), New VectorInt(0, 1), New VectorInt(-1, 0), New VectorInt(0, -1)} 'Stores the 4 positions that have to be checked for each piece.
                    Dim SpaceToCheckList As List(Of VectorInt) = New List(Of VectorInt) 'This list queues up all of the spaces to check

                    If MyPiece.Value = Piece.Scout Then 'If it's a scout queue up lots of spaces
                        SpaceToCheckList = GetAllScoutPositions(New VectorInt(x, y), WorkingBoard)
                    ElseIf Not MyPiece.Value = Piece.Flag AndAlso Not MyPiece.Value = Piece.Bomb Then 'No places to check if it's a 12 or a bomb.
                        For counter = 0 To 3 'Add only north, south, west, east
                            SpaceToCheckList.Add(NorthEastSouthWest(counter).Add(New VectorInt(x, y)))
                        Next
                    End If

                    For i = 0 To SpaceToCheckList.Count - 1 'KEEP IN MIND THAT 0 IS THE UNKNOWN VALUE
                        'Go through all 4 possibilities
                        Dim SpaceToCheck As VectorInt = SpaceToCheckList(i)
                        If InBounds(SpaceToCheck) Then 'Check if it's in the array

                            Dim MyMove As Move = New Move(New VectorInt(x, y), SpaceToCheck) 'This is the potential move
                            Dim PieceToCheck As BoardPiece = WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y)

                            If IsNothing(PieceToCheck) OrElse ((Utils.InRangeInc(1, 12, PieceToCheck.Value) AndAlso Utils.InRangeInc(1, 12, MyPiece.Value)) AndAlso Not PieceToCheck.IsRed = MyPiece.IsRed) Then 'Empty Space or both pieces are known
                                Dim NewChild As Node
                                Dim NewBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                If (NewBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                    NewChild = New MaxNode(NewBoard)
                                Else 'If it's the Player's turn, he is looking to minimise our utility
                                    NewChild = New MinNode(NewBoard)
                                End If
                                MyNode.AddChild(NewChild)
                            ElseIf (MyPiece.Value = 0 OrElse PieceToCheck.Value = 0) AndAlso Not PieceToCheck.IsRed = MyPiece.IsRed Then 'The piece moving is unknown and the pieces are on opposite teams.
                                Dim IntermediateNode As ChanceNode = New ChanceNode()

                                If CantSee(MyPiece) Then 'MyPiece is the attacking piece. If you can't see the piece do the following:
                                    Dim Rnd As New Random
                                    Dim RandomPieceValue As Integer = 0
                                    While RandomPieceValue = 0 OrElse RandomPieceValue = 11 OrElse RandomPieceValue = 12
                                        RandomPieceValue = WhoLoses(PieceToCheck.Value - 1)(Rnd.Next(0, WhoLoses(PieceToCheck.Value - 1).Length)) 'Select random piece AI could lose to.
                                        WorkingBoard.Pieces(x, y).Value = RandomPieceValue
                                    End While
                                    
                                    Dim TotalProbability As Double = 0
                                    For Each PieceValue In WhoLoses(PieceToCheck.Value - 1)
                                        TotalProbability += MyProbabilityMatrix(PieceValue - 1) 'Get total probability of loss
                                    Next

                                    Dim LoseChild As Node
                                    Dim LoseBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                    If (LoseBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        LoseChild = New MaxNode(LoseBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        LoseChild = New MinNode(LoseBoard)
                                    End If
                                    IntermediateNode.AddChild(LoseChild, TotalProbability) 'Add loss
                                    'Also add chance of win
                                    Dim WinChild As Node
                                    WorkingBoard.Pieces(x, y).Value = 1 'A 1 loses all the time
                                    Dim WinBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                    If (WinBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        WinChild = New MaxNode(WinBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        WinChild = New MinNode(WinBoard)
                                    End If
                                    IntermediateNode.AddChild(WinChild, 1 - TotalProbability) 'Add win
                                Else
                                    Dim Rnd As New Random 'This time we are attacking

                                    Dim RandomPieceValue As Integer = WhoLoses(MyPiece.Value - 1)(Rnd.Next(0, WhoLoses(MyPiece.Value - 1).Length)) 'Select random piece AI could lose to.
                                    WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y).Value = RandomPieceValue
                                    Dim TotalProbability As Double = 0

                                    For Each PieceValue In WhoLoses(MyPiece.Value - 1)
                                        TotalProbability += MyProbabilityMatrix(PieceValue - 1) 'Get total probability of loss
                                    Next

                                    Dim NewChild As Node
                                    Dim NewBoard As BoardState = WorkingBoard.Move(MyMove, True)

                                    If (NewBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        NewChild = New MaxNode(NewBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        NewChild = New MinNode(NewBoard)
                                    End If
                                    IntermediateNode.AddChild(NewChild, TotalProbability) 'Add loss

                                    'Also add chance of winning
                                    Dim WinChild As Node
                                    WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y).Value = 1 'A 1 loses to everything
                                    Dim WinBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                    If (WinBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        WinChild = New MaxNode(WinBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        WinChild = New MinNode(WinBoard)
                                    End If
                                    IntermediateNode.AddChild(WinChild, 1 - TotalProbability) 'Add win
                                End If

                                If CantSee(MyPiece) Then
                                    WorkingBoard.Pieces(x, y).Value = 0
                                Else
                                    WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y).Value = 0
                                End If

                                MyNode.AddChild(IntermediateNode)
                            End If


                        End If
                    Next
                End If
            Next
        Next
    End Sub

    Function FindChildrenMoves(ByRef MyNode As Node) As List(Of Move) 'Finds the children of a node and adds them(Only 1 level deep)
        Dim MovesList As New List(Of Move)
        Dim WorkingBoard As BoardState = MyNode.MyBoardState 'The board we are working with.
        Dim MyProbabilityMatrix As Double() = GenerateProbabilityMatrix()
        For x = 0 To Utils.BoardSide - 1
            For y = 0 To Utils.BoardSide - 1 'Look at every piece
                If Not IsNothing(WorkingBoard.Pieces(x, y)) AndAlso WorkingBoard.Pieces(x, y).IsRed = MyNode.MyBoardState.IsRedToPlay Then 'The piece must meet these coditions

                    Dim MyPiece As BoardPiece = WorkingBoard.Pieces(x, y)
                    Dim NorthEastSouthWest() As VectorInt = {New VectorInt(1, 0), New VectorInt(0, 1), New VectorInt(-1, 0), New VectorInt(0, -1)} 'Stores the 4 positions that have to be checked for each piece.
                    Dim SpaceToCheckList As List(Of VectorInt) = New List(Of VectorInt) 'This list queues up all of the spaces to check

                    If MyPiece.Value = Piece.Scout Then 'If it's a scout queue up lots of spaces
                        SpaceToCheckList = GetAllScoutPositions(New VectorInt(x, y), WorkingBoard)
                    ElseIf Not MyPiece.Value = Piece.Flag AndAlso Not MyPiece.Value = Piece.Bomb Then 'No places to check if it's a 12 or a bomb.
                        For counter = 0 To 3 'Add only north, south, west, east
                            SpaceToCheckList.Add(NorthEastSouthWest(counter).Add(New VectorInt(x, y)))
                        Next
                    End If

                    For i = 0 To SpaceToCheckList.Count - 1 'KEEP IN MIND THAT 0 IS THE UNKNOWN VALUE
                        'Go through all 4 possibilities
                        Dim SpaceToCheck As VectorInt = SpaceToCheckList(i)
                        If InBounds(SpaceToCheck) Then 'Check if it's in the array

                            Dim MyMove As Move = New Move(New VectorInt(x, y), SpaceToCheck) 'This is the potential move
                            Dim PieceToCheck As BoardPiece = WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y)

                            If IsNothing(PieceToCheck) OrElse ((Utils.InRangeInc(1, 12, PieceToCheck.Value) AndAlso Utils.InRangeInc(1, 12, MyPiece.Value)) AndAlso Not PieceToCheck.IsRed = MyPiece.IsRed) Then 'Empty Space or both pieces are known
                                Dim NewChild As Node
                                Dim NewBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                If (NewBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                    NewChild = New MaxNode(NewBoard)
                                Else 'If it's the Player's turn, he is looking to minimise our utility
                                    NewChild = New MinNode(NewBoard)
                                End If
                                MyNode.AddChild(NewChild)
                                MovesList.Add(MyMove) 'Add coressponding move
                            ElseIf (MyPiece.Value = 0 OrElse PieceToCheck.Value = 0) AndAlso Not PieceToCheck.IsRed = MyPiece.IsRed Then 'The piece moving is unknown and the pieces are on opposite teams.
                                Dim IntermediateNode As ChanceNode = New ChanceNode()

                                If CantSee(MyPiece) Then 'MyPiece is the attacking piece. If you can't see the piece do the following:
                                    Dim Rnd As New Random
                                    Dim RandomPieceValue As Integer = 0
                                    While RandomPieceValue = 0 OrElse RandomPieceValue = 11 OrElse RandomPieceValue = 12
                                        RandomPieceValue = WhoLoses(PieceToCheck.Value - 1)(Rnd.Next(0, WhoLoses(PieceToCheck.Value - 1).Length)) 'Select random piece AI could lose to.
                                        WorkingBoard.Pieces(x, y).Value = RandomPieceValue
                                    End While

                                    Dim TotalProbability As Double = 0
                                    For Each PieceValue In WhoLoses(PieceToCheck.Value - 1)
                                        TotalProbability += MyProbabilityMatrix(PieceValue - 1) 'Get total probability of loss
                                    Next

                                    Dim LoseChild As Node
                                    Dim LoseBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                    If (LoseBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        LoseChild = New MaxNode(LoseBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        LoseChild = New MinNode(LoseBoard)
                                    End If
                                    IntermediateNode.AddChild(LoseChild, TotalProbability) 'Add loss
                                    'Also add chance of win
                                    Dim WinChild As Node
                                    WorkingBoard.Pieces(x, y).Value = 1 'A 1 loses all the time
                                    Dim WinBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                    If (WinBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        WinChild = New MaxNode(WinBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        WinChild = New MinNode(WinBoard)
                                    End If
                                    IntermediateNode.AddChild(WinChild, 1 - TotalProbability) 'Add win
                                Else
                                    Dim Rnd As New Random 'This time we are attacking

                                    Dim RandomPieceValue As Integer = WhoLoses(MyPiece.Value - 1)(Rnd.Next(0, WhoLoses(MyPiece.Value - 1).Length)) 'Select random piece AI could lose to.
                                    WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y).Value = RandomPieceValue
                                    Dim TotalProbability As Double = 0

                                    For Each PieceValue In WhoLoses(MyPiece.Value - 1)
                                        TotalProbability += MyProbabilityMatrix(PieceValue - 1) 'Get total probability of loss
                                    Next

                                    Dim NewChild As Node
                                    Dim NewBoard As BoardState = WorkingBoard.Move(MyMove, True)

                                    If (NewBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        NewChild = New MaxNode(NewBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        NewChild = New MinNode(NewBoard)
                                    End If
                                    IntermediateNode.AddChild(NewChild, TotalProbability) 'Add loss

                                    'Also add chance of winning
                                    Dim WinChild As Node
                                    WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y).Value = 1 'A 1 loses to everything
                                    Dim WinBoard As BoardState = WorkingBoard.Move(MyMove, True)
                                    If (WinBoard.IsRedToPlay = IsAIRedTeam) Then 'If it's the AIs turn, we are looking to max
                                        WinChild = New MaxNode(WinBoard)
                                    Else 'If it's the Player's turn, he is looking to minimise our utility
                                        WinChild = New MinNode(WinBoard)
                                    End If
                                    IntermediateNode.AddChild(WinChild, 1 - TotalProbability) 'Add win
                                End If

                                If CantSee(MyPiece) Then
                                    WorkingBoard.Pieces(x, y).Value = 0
                                Else
                                    WorkingBoard.Pieces(SpaceToCheck.x, SpaceToCheck.y).Value = 0
                                End If

                                MyNode.AddChild(IntermediateNode)
                                MovesList.Add(MyMove) 'Add coressponding move
                            End If
                        End If
                    Next
                End If
            Next
        Next
        Return MovesList
    End Function

    Function CantSee(ByVal PieceToCheck As BoardPiece) As Boolean 'Checks if a given piece is not visible to the AI
        Return (PieceToCheck.Hidden = True) AndAlso Not (PieceToCheck.IsRed = IsAIRedTeam)
    End Function

    Function GenerateProbabilityMatrix() As Double()
        Dim ReturnMatrix(11) As Double 'Stores the probability of each piece. So the probability of a 1 is at position 0
        For i = 0 To PieceFreq.Count - 1
            ReturnMatrix(PieceFreq(i) - 1) += 1 'Ex: It find a 1 in PieceFreq so it adds one to the 0th position
        Next
        For i = 0 To 11
            ReturnMatrix(i) = ReturnMatrix(i) / PieceFreq.Count 'Divide each piece by the total to turn it into probabilities
        Next
        Return ReturnMatrix
    End Function

    Sub PlaceRelativeToFlag(ByRef BoardArray(,) As BoardPiece, ByVal Strategies() As Strategy, ByVal FlagPos As VectorInt) 'Places the pieces relative to the flag
        For i = 0 To PieceFreq.Count - 2 'Minus 2 because we don't want to place the flag.
            Dim RelatIvePiecePos As VectorInt
            Dim RealPiecePos As VectorInt
            Do
                RelatIvePiecePos = Strategies(PieceFreq(i) - 1).PickOne()
                RealPiecePos = RelatIvePiecePos.Add(FlagPos) 'Add flag vector

                If RealPiecePos.x > 9 Then 'Make sure that it isn't overflowing
                    Dim StepBack As Integer = 2 * (RealPiecePos.x) - 9 - FlagPos.x
                    RealPiecePos.x = RealPiecePos.x - StepBack
                End If
                If RealPiecePos.y > 3 Then 'Make sure that it isn't overflowing
                    Dim StepBack As Integer = 2 * (RealPiecePos.y) - 3 - FlagPos.y
                    RealPiecePos.y = RealPiecePos.y - StepBack
                End If
            Loop Until IsNothing(BoardArray(RealPiecePos.x, RealPiecePos.y))
            'Make sure that the piece isn't overwriting an existing piece. Keep trying until it works.
            Dim PieceToAdd As BoardPiece = New BoardPiece(PieceFreq(i), IsAIRedTeam)
            BoardArray(RealPiecePos.x, RealPiecePos.y) = PieceToAdd
            PieceHistories(i) = New PieceHistory(New VectorInt(RealPiecePos.x, RealPiecePos.y + 6), PieceToAdd) 'Record Initial Position
        Next
    End Sub

    Function NumberToVector(ByVal MyInt As Integer) As VectorInt '?
        Return New VectorInt(MyInt Mod 10, Math.Floor(MyInt / 10))
    End Function

End Module
