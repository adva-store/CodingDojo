import Head from "next/head";
import Viewer from "@/components/viewer/viewer";

export default function Home() {
  return (
    <>
      <Head>
        <title>Berlin Clock</title>
        <meta name="description" content="Berlin Time" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <main>
        <Viewer></Viewer>
      </main>
    </>
  );
}
