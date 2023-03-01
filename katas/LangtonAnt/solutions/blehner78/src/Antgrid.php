<?php
declare(strict_types = 1);

class Antgrid
{

    private $canvas;
    private $width;
    private $height;

    public function __construct(int $width = 200, int $height=200)
    {
        $this->width = $width;
        $this->height = $height;
        $this->canvas = array();
    }
    
    public function isActive(int $x, int $y): bool
    {
        return $this->canvas->get($x, $y);
    }
    
    public function toggle(int $x, int $y): void
    {
        $this->canvas->toggle($x, $y);
    }
    
    public function getWidth(): int
    {
        return $this->width;
    }
    
    public function getHeight(): int
    {
        return $this->height;
    }

    // drawCanvas
    // exportImage
}