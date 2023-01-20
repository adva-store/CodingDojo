// Next.js API route support: https://nextjs.org/docs/api-routes/introduction
import { BerlinClockTime } from "../../models/berlin-clock";
import type { NextApiRequest, NextApiResponse } from "next";
import { getBerlineClockTime } from "../../utils/time";

/**
 * Returns the current time in berlin clock format
 */
export default function handler(
  req: NextApiRequest,
  res: NextApiResponse<BerlinClockTime>
) {
  if (req.method === "GET") {
    const time = getBerlineClockTime(new Date());
    res.status(200).json(time);
  } else {
    res.status(405);
  }
}
