<?php
declare(strict_types = 1);


class Ant
{

    private $antgrid;
    private $x;
    private $y;
    private $direction;
    private $compass = ['NORTH', 'EAST', 'SOUTH', 'WEST'];

    public function __construct(Antgrid $antgrid)
    {
        $this->antgrid = $antgrid;
        $this->x = (int) ($antgrid->getWidth() / 2);
        $this->y = (int) ($antgrid->getHeight() / 2);
        $this->direction = 0;
        $this->steps = 0;
    }

    public function step()
    {
        if($this->antgrid->isActive($this->x, $this->y)) {
            $this->turnRight();
        } else {
            $this->turnLeft();
        }
        $this->antgrid->toggle($this->x, $this->y);
        $this->makeMove();
        $this->steps++;
    }

    private function turnRight()
    {
        $this->direction = $this->modulo($this->direction + 1, 4);
    }

    private function turnLeft()
    {
        $this->direction = $this->modulo($this->direction - 1, 4);
    }

    private function makeMove()
    {
        switch ($this->compass[$this->direction]) {
            case 'NORTH':
                $this->y++;
                break;
            case 'EAST':
                $this->x++;
                break;
            case 'SOUTH':
                $this->y--;
                break;
            case 'WEST':
                $this->x--;
                break;
        }

        $this->x = $this->modulo($this->x, $this->antgrid->getWidth());
        $this->y = $this->modulo($this->y, $this->antgrid->getHeight());
    }

    private function modulo($a, $b)
    {
        $a %= $b;
        if($a<0) {
            $a += abs($b);
        }

        return $a;
    }

}