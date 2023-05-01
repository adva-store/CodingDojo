<?php

namespace App\Http\Controllers\LangtonAnt;

use App\Models\Ant;
use App\Models\Grid;
use Illuminate\Http\Request;

trait Values
{
    public function getPosition(Request $request, string $key): int
    {
        return (int) min(
            $request->get($key, Grid::DEFAULT_POSITION),
            $this->getSize($request) - 1
        );
    }

    public function getDirection(Request $request): string
    {
        return match ($request->get('direction')) {
            Ant::DIRECTION_LEFT => Ant::DIRECTION_LEFT,
            Ant::DIRECTION_RIGHT => Ant::DIRECTION_RIGHT,
            Ant::DIRECTION_TOP => Ant::DIRECTION_TOP,
            Ant::DIRECTION_BOTTOM => Ant::DIRECTION_BOTTOM,
            default => Ant::DEFAULT_DIRECTION
        };
    }

    public function getSize(Request $request): int
    {
        return $request->get('size', Grid::DEFAULT_SIZE);
    }

    public function getMoveCounter(Request $request): int
    {
        return $request->get('move_counter', Grid::DEFAULT_MOVE_COUNTER);
    }

    public function getColors(Request $request): array
    {
        $colors = [];
        $size = $this->getSize($request);
        $defaultColor = $this->getDefaultColor($request);

        for ($y = 0; $y < $size; ++$y) {
            for ($x = 0; $x < $size; ++$x) {
                $colors[$y][$x] = $defaultColor;
            }
        }

        return $colors;
    }

    public function getDefaultColor(Request $request): string
    {
        return match ($request->get('color')) {
            Grid::BLACK => Grid::BLACK,
            Grid::WHITE => Grid::WHITE,
            default => Grid::DEFAULT_COLOR
        };
    }
}
