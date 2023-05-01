<?php


class LangtonAnt{
    private const TILE_COLOR_BLACK = 's';
    private const TILE_COLOR_WHITE = 'w';
    
    private const ANT_DIRECTION_NORTH = 'n';
    private const ANT_DIRECTION_EAST = 'o';
    private const ANT_DIRECTION_SOUTH = 's';
    private const ANT_DIRECTION_WEST = 'w';
    
    protected array $grid = [];
    
    protected array $gridSteps = [];
    
    protected int $antHeight = 0;
    protected int $antWidth = 0;
    protected string $antDirection = self::ANT_DIRECTION_NORTH;
    protected int $stepCount = 0;
    
    protected array $turnAntRight = [
        self::ANT_DIRECTION_NORTH => self::ANT_DIRECTION_EAST,
        self::ANT_DIRECTION_EAST => self::ANT_DIRECTION_SOUTH,
        self::ANT_DIRECTION_SOUTH => self::ANT_DIRECTION_WEST,
        self::ANT_DIRECTION_WEST => self::ANT_DIRECTION_NORTH,
    ];
    
    public function __construct(int $gridLength, int $antStartPositionX, int $antStartPostionY, string $antDirection, int $stepCount) {
        $this->antHeight = $antStartPositionX;
        $this->antWidth = $antStartPostionY;
        $this->antDirection = $antDirection;
        $this->stepCount = $stepCount;
        for($i = 1; $i <= $gridLength; $i++){
            for($j = 1; $j <= $gridLength; $j++){
                $this->grid[$i][$j] = self::TILE_COLOR_WHITE;
            }
        }
        
    }

    public function getSteps()
    {
        return $this->gridSteps;
    }
    
    public function executeSteps()
    {
        for($step = 1; $step <= $this->stepCount; $step++){
           $this->executeStep();
           $this->implodeGridForStep();
        }
    }
    
    protected function implodeGridForStep() {
        $grid = $this->grid;
        $implodedRows = [];
        $grid[$this->antHeight][$this->antWidth] = $this->antDirection . $this->grid[$this->antHeight][$this->antWidth];

        foreach($grid as $row){
            $implodedRows[] = implode(',', $row);
        }
        $this->gridSteps[] = implode(',', $implodedRows);
    }
    
    protected function executeStep() {
        $currentFieldColor = $this->grid[$this->antHeight][$this->antWidth];
        if($currentFieldColor === self::TILE_COLOR_WHITE){
            $this->grid[$this->antHeight][$this->antWidth] = self::TILE_COLOR_BLACK;
        } else {
            $this->grid[$this->antHeight][$this->antWidth] = self::TILE_COLOR_WHITE;
        }

        $this->turnAnt($currentFieldColor);
        
        switch ($this->antDirection){
            case self::ANT_DIRECTION_NORTH:
                $this->antHeight--;
                break;
            case self::ANT_DIRECTION_SOUTH:
                $this->antHeight++;
                break;
            case self::ANT_DIRECTION_EAST:
                $this->antWidth++;
                break;
            case self::ANT_DIRECTION_WEST:
                $this->antWidth--;
                break;
        }
    }
    
    protected function turnAnt(string $currentFieldColor)
    {
        if($currentFieldColor === self::TILE_COLOR_WHITE){
            $this->antDirection = $this->turnAntRight[$this->antDirection];
        } else {
            $turnAntLeft = array_flip($this->turnAntRight);
            $this->antDirection = $turnAntLeft[$this->antDirection];
        }
    }
}