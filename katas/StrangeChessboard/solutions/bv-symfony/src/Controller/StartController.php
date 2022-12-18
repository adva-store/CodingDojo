<?php

namespace App\Controller;

use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Request;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\Routing\Annotation\Route;
use App\Service\ChessService;

class StartController extends AbstractController
{
    private $chessService;

    public function __construct(ChessService $chessService)
    {
        $this->chessService = $chessService;
    }

    /**
     * Summary of index
     * @Route("/", name="app_start")
     * @return Response
     */
    public function index(): Response
    {
        return $this->render('start/index.html.twig', []);
    }

    /**
     * @Route("/calculate", name="app_calculate")
     */
    public function calculate(Request $request): Response
    {
        $cs = $request->request->get('cs');
        $rs = $request->request->get('rs');
        $columns = explode(',', $cs);
        $rows = explode(',', $rs);

        $result = $this->chessService->calculateResult($columns, $rows);

        return $this->render('start/calculate.html.twig', [
            'controller_name' => 'StartController',
            'result' => $result,
        ]);
    }

    /**
     * Summary of print
     * @Route("/print", name="app_print")
     * @param Request $request
     * @return Response
     */
    public function print(Request $request): Response
    {
        $cs = $request->request->get('cs');
        $rs = $request->request->get('rs');
        $columns = explode(',', $cs);
        $rows = explode(',', $rs);

        list($printWidth, $coefficient, $sorted) = $this->chessService->printResult($columns, $rows);

        return $this->render('start/print.html.twig', [
            'controller_name' => 'StartController',
            'sorted' => $sorted,
            'printWidth' => $printWidth,
            'coefficient' => $coefficient,
        ]);
    }
}
