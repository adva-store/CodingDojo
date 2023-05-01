<?php

namespace App\Services\LangtonAnt;

use App\Models\Ant;
use App\Models\Grid;

class Walker
{
    public function rotate(Grid $grid, Ant $ant): Walker
    {
        $isWhite = Grid::WHITE == $grid->colors[$ant->position_y][$ant->position_x];

        $ant->direction = match ($ant->direction) {
            Ant::DIRECTION_TOP => $isWhite ? Ant::DIRECTION_RIGHT : Ant::DIRECTION_LEFT,
            Ant::DIRECTION_BOTTOM => $isWhite ? Ant::DIRECTION_LEFT : Ant::DIRECTION_RIGHT,
            Ant::DIRECTION_RIGHT => $isWhite ? Ant::DIRECTION_BOTTOM : Ant::DIRECTION_TOP,
            Ant::DIRECTION_LEFT => $isWhite ? Ant::DIRECTION_TOP : Ant::DIRECTION_BOTTOM,
        };

        return $this;
    }

    public function colorize(Grid $grid, Ant $ant): Walker
    {
        $colors = $grid->colors;

        $newColor = match (
            $grid->colors[$ant->position_y][$ant->position_x]
        ) {
            Grid::BLACK => Grid::WHITE,
            Grid::WHITE => Grid::BLACK,
        };
        $colors[$ant->position_y][$ant->position_x] = $newColor;
        $grid->colors = $colors;

        return $this;
    }

    public function walk(Grid $grid, Ant $ant): Walker
    {
        ++$grid->move_counter;

        switch ($ant->direction) {
            case Ant::DIRECTION_TOP:
                --$ant->position_y;
                break;

            case Ant::DIRECTION_BOTTOM:
                ++$ant->position_y;
                break;

            case Ant::DIRECTION_RIGHT:
                ++$ant->position_x;
                break;

            case Ant::DIRECTION_LEFT:
                --$ant->position_x;
                break;
        }

        return $this;
    }

    public function endOfWalk(Grid $grid, Ant $ant): bool
    {
        return $ant->position_y < 0
            || $ant->position_x < 0
            || $ant->position_y >= $grid->size
            || $ant->position_x >= $grid->size;
    }
}
